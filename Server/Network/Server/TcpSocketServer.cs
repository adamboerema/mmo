using System;
using System.Net;
using System.Net.Sockets;
using Common.Bus;
using Common.Network;
using Common.Network.Shared;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Server.Configuration;
using Server.Engine.Player;
using Server.Network.Connection;

namespace Server.Network.Server
{
    public class TcpSocketServer: IServer, IEventBusListener<ConnectionEvent>
    {
        private readonly IConnectionManager _connectionManager;
        private readonly IPlayerManager _playerManager;
        private readonly IReceiverPacketBus _receiverPacketBus;
        private readonly IConnectionBus _connectionBus;
        private readonly TcpListener _socket;
        private readonly StateBuffer _stateBuffer;

        public TcpSocketServer(
            IServerConfiguration configuration,
            IConnectionManager connectionManager,
            IPlayerManager playerComponent,
            IConnectionBus connectionBus,
            IReceiverPacketBus receiverPacketBus)
        {
            _connectionManager = connectionManager;
            _playerManager = playerComponent;
            _connectionBus = connectionBus;
            _receiverPacketBus = receiverPacketBus;
            _socket = new TcpListener(IPAddress.Any, configuration.Port);
            _stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);
        }

        public void Start()
        {
            _socket.Start();
            _connectionBus.Subscribe(this);
            _socket.BeginAcceptTcpClient(
                new AsyncCallback(HandleClientConnect),
                _stateBuffer);
        }

        public void Close()
        {
            _connectionBus.Unsubscribe(this);
            _connectionManager.CloseAllConnections();
        }

        private void HandleClientConnect(IAsyncResult result)
        {
            var currentStateBuffer = result.AsyncState as StateBuffer;
            var clientSocket = _socket.EndAcceptTcpClient(result);

            // Store client in memory
            var uniqueId = Guid.NewGuid().ToString();
            var connection = new TcpSocketConnection(
                uniqueId,
                clientSocket,
                _connectionBus,
                _receiverPacketBus);

            _connectionManager.AddConnection(connection);
            connection.Start();

            // Allow for next client connection
            _socket.BeginAcceptTcpClient(
                new AsyncCallback(HandleClientConnect),
                currentStateBuffer);
        }

        public void Handle(ConnectionEvent eventObject)
        {
            var connectionId = eventObject.Id;
            switch (eventObject.State)
            {
                case ConnectionState.CONNECT:
                    Console.WriteLine($"Client Connected: {connectionId}");
                    _playerManager.InitializePlayer(connectionId);
                    break;
                case ConnectionState.DISCONNECT:
                    Console.WriteLine($"Client Disconnected: {connectionId}");
                    _playerManager.RemovePlayer(connectionId);
                    _connectionManager.CloseConnection(connectionId);
                    break;
            }
        }
    }
}
