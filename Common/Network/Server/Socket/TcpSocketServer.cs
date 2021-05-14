using System;
using System.Net;
using System.Net.Sockets;
using Common.Network.Server.Manager;
using Common.Network.Shared;

namespace Common.Network.Server.Socket
{
    public class TcpSocketServer: IServer
    {
        private IConnectionManager connectionManager;
        private readonly TcpListener socket;
        private readonly StateBuffer stateBuffer;
        private byte[] buffer;

        public TcpSocketServer(int port)
        {
            connectionManager = new ConnectionManager(Constants.MAX_PLAYERS);
            socket = new TcpListener(IPAddress.Any, port);
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
            var connection = new Connection(uniqueId, clientSocket);
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
