using System;
using System.Net;
using System.Net.Sockets;
using Common.Network;
using Common.Network.Shared;
using Server.Configuration;
using Server.Network.Connection;

namespace Server.Network.Server
{
    public class TcpSocketServer: IServer
    {
        private IConnectionManager connectionManager;
        private IConnectionReceiver connectionReceiver;
        private readonly TcpListener socket;
        private readonly StateBuffer stateBuffer;

        public TcpSocketServer(
            IServerConfiguration configuration,
            IConnectionManager connectionManager,
            IConnectionReceiver connectionReceiver)
        {
            this.connectionManager = connectionManager;
            socket = new TcpListener(IPAddress.Any, configuration.Port);
            stateBuffer = new StateBuffer(Constants.BUFFER_STATE_SIZE);
        }

        public void Start()
        {
            socket.Start();
            socket.BeginAcceptTcpClient(
                new AsyncCallback(HandleClientConnect),
                stateBuffer);
        }

        public void CloseConnection()
        {
            connectionManager.CloseAllConnections();
        }

        private void HandleClientConnect(IAsyncResult result)
        {
            var currentStateBuffer = result.AsyncState as StateBuffer;
            var clientSocket = socket.EndAcceptTcpClient(result);

            // Store client in memory
            var uniqueId = Guid.NewGuid().ToString();
            var connection = new TcpSocketConnection(uniqueId, clientSocket, connectionReceiver);
            connection.Start();
            connectionManager.AddConnection(connection);

            Console.WriteLine($"Connected client connection: {uniqueId}");

            // Allow for next client connection
            socket.BeginAcceptTcpClient(
                new AsyncCallback(HandleClientConnect),
                currentStateBuffer);
        }
    }
}
