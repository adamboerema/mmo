using System;
using System.Net.Sockets;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Manager;
using Common.Network.Shared;

namespace Common.Network.Server.Socket
{
    public class Connection: IConnection
    {
        public string Id => id;

        private string id;
        private TcpClient socket;
        private StateBuffer stateBuffer;
        private NetworkStream stream;
        private IPacketManager packetManager;
        private byte[] readBuffer;

        public Connection(string connectionId, TcpClient connectionSocket)
        {
            id = connectionId;
            socket = connectionSocket;
            socket.NoDelay = true;
            socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;
            readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];
            stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);
            packetManager = new PacketManager();
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
