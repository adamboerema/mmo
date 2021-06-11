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
        private readonly TcpClient _socket;
        private readonly IPacketManager _packetManager;
        private readonly IPAddress _remoteIPAddress;
        private readonly int _remotePort;
        private StateBuffer _stateBuffer;
        private NetworkStream _stream;
        private byte[] _readBuffer;

        public TcpSocketClient(string ipAddress, int port)
        {
            _remoteIPAddress = IPAddress.Parse(ipAddress);
            _remotePort = port;
            _readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];
            _stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);

            var packetDefinitions = new Definitions();
            _packetManager = new PacketManager(packetDefinitions);

            _socket = new TcpClient();
            _socket.NoDelay = false;
            _socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            _socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;

        }

        /// <summary>
        /// Start the client connection
        /// </summary>
        public async Task Start()
        {
            await _socket.ConnectAsync(_remoteIPAddress, _remotePort);
            _stream = _socket.GetStream();
            BeginStreamRead(_stateBuffer);
        }

        /// <summary>
        /// Sends data to the stream
        /// </summary>
        /// <param name="data">Byte array data</param>
        public void Send(IPacket packet) {
            if(_socket.Connected)
            {
                var packetBytes = _packetManager.Write(packet);
                _stream.Write(packetBytes, 0, packetBytes.Length);
            }
        }

        /// <summary>
        /// Close the socket connection
        /// </summary>
        public void CloseSocket()
        {
            _socket.Close();
        }

        /// <summary>
        /// Handle the receive data callback
        /// </summary>
        /// <param name="result"></param>
        private void HandleDataReceive(IAsyncResult result)
        {
            var offset = 0;
            var readByteCount = _stream.EndRead(result);
            if(readByteCount <= 0)
            {
                CloseSocket();
            }

            var packetBytes = new byte[readByteCount];
            Buffer.BlockCopy(
                _readBuffer,
                offset,
                packetBytes,
                offset,
                readByteCount);
            var packet = _packetManager.Receive(packetBytes);

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
            _stream.BeginRead(
                _readBuffer,
                offset,
                Constants.BUFFER_CLIENT_SIZE,
                new AsyncCallback(HandleDataReceive),
                state);
        }
    }
}
