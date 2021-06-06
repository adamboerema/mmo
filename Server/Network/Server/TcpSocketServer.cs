using System;
using System.Net;
using System.Net.Sockets;
using Common.Network;
using Common.Network.Shared;
using Server.Bus.Packet;
using Server.Configuration;
using Server.Network.Connection;

namespace Server.Network.Server
{
    public class TcpSocketServer: IServer
    {
        private IConnectionManager _connectionManager;
        private IReceiverPacketBus _receiverPacketBus;
        private readonly TcpListener _socket;
        private readonly StateBuffer _stateBuffer;

        public TcpSocketServer(
            IServerConfiguration configuration,
            IConnectionManager connectionManager,
            IReceiverPacketBus receiverPacketBus)
        {
            _connectionManager = connectionManager;
            _receiverPacketBus = receiverPacketBus;
            _socket = new TcpListener(IPAddress.Any, configuration.Port);
            _stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);
        }

        public void Start()
        {
            _socket.Start();
            _socket.BeginAcceptTcpClient(
                new AsyncCallback(HandleClientConnect),
                _stateBuffer);
        }

        public void CloseConnection()
        {
            _connectionManager.CloseAllConnections();
        }

        private void HandleClientConnect(IAsyncResult result)
        {
            var currentStateBuffer = result.AsyncState as StateBuffer;
            var clientSocket = _socket.EndAcceptTcpClient(result);

            // Store client in memory
            var uniqueId = Guid.NewGuid().ToString();
            var connection = new TcpSocketConnection(uniqueId, clientSocket, _receiverPacketBus);
            connection.Start();
            _connectionManager.AddConnection(connection);

            Console.WriteLine($"Connected client connection: {uniqueId}");

            // Allow for next client connection
            _socket.BeginAcceptTcpClient(
                new AsyncCallback(HandleClientConnect),
                currentStateBuffer);
        }
    }
}
