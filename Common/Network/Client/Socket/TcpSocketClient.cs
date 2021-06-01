using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Network.Client.Socket;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Manager;
using Common.Network.Shared;

namespace Common.Network.Client
{
    public class TcpSocketClient : IClient
    {
        private readonly TcpClient socket;
        private readonly IPacketManager packetManager;
        private readonly IPAddress remoteIPAddress;
        private readonly int remotePort;
        private StateBuffer stateBuffer;
        private NetworkStream stream;
        private byte[] readBuffer;

        public TcpSocketClient(string ipAddress, int port)
        {
            remoteIPAddress = IPAddress.Parse(ipAddress);
            remotePort = port;
            readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];
            stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);

            var packetDefinitions = new Definitions();
            packetManager = new PacketManager(packetDefinitions);

            socket = new TcpClient();
            socket.NoDelay = false;
            socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;

        }

        /// <summary>
        /// Start the client connection
        /// </summary>
        public async Task Start()
        {
            await socket.ConnectAsync(remoteIPAddress, remotePort);
            stream = socket.GetStream();
            BeginStreamRead(stateBuffer);
        }

        /// <summary>
        /// Sends data to the stream
        /// </summary>
        /// <param name="data">Byte array data</param>
        public void Send(IPacket packet) {
            if(socket.Connected)
            {
                var packetBytes = packetManager.Write(packet);
                stream.Write(packetBytes, 0, packetBytes.Length);
            }
        }

        /// <summary>
        /// Close the socket connection
        /// </summary>
        public void CloseSocket()
        {
            socket.Close();
        }

        /// <summary>
        /// Handle the receive data callback
        /// </summary>
        /// <param name="result"></param>
        private void HandleDataReceive(IAsyncResult result)
        {
            var offset = 0;
            var readByteCount = stream.EndRead(result);
            if(readByteCount <= 0)
            {
                CloseSocket();
            }

            var packetBytes = new byte[readByteCount];
            Buffer.BlockCopy(
                readBuffer,
                offset,
                packetBytes,
                offset,
                readByteCount);
            var packet = packetManager.Receive(packetBytes);

            Console.WriteLine($"Packet {packet.Id}: {packet}");
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
