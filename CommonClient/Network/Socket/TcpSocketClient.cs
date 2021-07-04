using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Network.Shared;
using Common.Network;
using CommonClient.Bus.Packet;
using Common.Bus;
using Common.Network.Manager;
using Common.Definitions;

namespace CommonClient.Network.Socket
{
    public class TcpSocketClient : IClient, IEventBusListener<PacketEvent>
    {
        private readonly TcpClient _socket;
        private readonly IPacketManager _packetManager;
        private readonly IReceiverPacketBus _receiverPacketBus;
        private readonly IDispatchPacketBus _dispatchPacketBus;

        private NetworkStream _stream;
        private StateBuffer _stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);
        private byte[] _readBuffer = new byte[Constants.BUFFER_CLIENT_SIZE];

        public TcpSocketClient(
            IReceiverPacketBus receiverPacketBus,
            IDispatchPacketBus dispatchPacketBus)
        {
            _receiverPacketBus = receiverPacketBus;
            _dispatchPacketBus = dispatchPacketBus;
            _dispatchPacketBus.Subscribe(this);

            var packetDefinitions = new PacketDefinitions();
            _packetManager = new PacketManager(packetDefinitions);

            _socket = new TcpClient();
            _socket.NoDelay = false;
            _socket.SendBufferSize = Constants.BUFFER_CLIENT_SIZE;
            _socket.ReceiveBufferSize = Constants.BUFFER_CLIENT_SIZE;

        }

        /// <summary>
        /// Start the client connection
        /// </summary>
        public async Task Start(string ipAddress, int port)
        {
            await _socket.ConnectAsync(ipAddress, port);
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
        public void Close()
        {
            _socket.Close();
            _dispatchPacketBus.Unsubscribe(this);
        }

        /// <summary>
        /// Handle the incoming packets
        /// </summary>
        /// <param name="eventObject"></param>
        public void Handle(PacketEvent eventObject)
        {
            Send(eventObject.Packet);
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
                Close();
                return;
            }

            var packetBytes = new byte[readByteCount];
            Buffer.BlockCopy(
                _readBuffer,
                offset,
                packetBytes,
                offset,
                readByteCount);
            var packet = _packetManager.Receive(packetBytes);
            _receiverPacketBus.Publish(packet);
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
