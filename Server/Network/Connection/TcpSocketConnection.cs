using System;
using System.Net.Sockets;
using Common.Network;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Manager;
using Common.Network.Shared;

namespace Server.Network.Connection
{
    public class TcpSocketConnection: IConnection
    {
        public string Id => id;

        private string id;
        private readonly TcpClient socket;
        private readonly StateBuffer stateBuffer;
        private readonly IPacketManager packetManager;
        private readonly IConnectionReceiver receiver;
        private NetworkStream stream;
        private byte[] readBuffer;

        public TcpSocketConnection(
            string connectionId,
            TcpClient connectionSocket,
            IConnectionReceiver connectionReceiver)
        {
            id = connectionId;
            socket = connectionSocket;
            socket.NoDelay = true;
            socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;
            readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];
            stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);

            var serverDefinitions = new Definitions();
            packetManager = new PacketManager(serverDefinitions);
            receiver = connectionReceiver;
        }

        public void Start()
        {
            stream = socket.GetStream();
            BeginStreamRead(stateBuffer);
        }

        public void CloseConnection()
        {
            socket.Close();
        }

        public void Send(IPacket packet)
        {
            var writeBytes = packetManager.Write(packet);
            stream.Write(writeBytes, 0, writeBytes.Length);
        }

        /// <summary>
        /// Handle the receive data callback
        /// </summary>
        /// <param name="result"></param>
        private void HandleDataReceive(IAsyncResult result)
        {
            var offset = 0;
            var readByteCount = stream.EndRead(result);
            if (readByteCount <= 0)
            {
                CloseConnection();
            }

            var packetBytes = new byte[readByteCount];
            Buffer.BlockCopy(
                readBuffer,
                offset,
                packetBytes,
                offset,
                readByteCount);

            var packet = packetManager.Receive(packetBytes);
            receiver.Receive(Id, packet);
            BeginStreamRead(result.AsyncState as StateBuffer);
        }

        /// <summary>
        /// Begin a stream read with state object
        /// </summary>
        /// <param name="state">State object</param>
        private void BeginStreamRead(StateBuffer state)
        {
            var offset = 0;
            stream.BeginRead(
                readBuffer,
                offset,
                Constants.BUFFER_CLIENT_SIZE,
                new AsyncCallback(HandleDataReceive),
                state);
        }
    }
}
