using System;
using Server.Configuration;
using Server.Network.Connection;
using Server.Network.Server;

namespace Sever
{
    public class GameServer
    {
        private readonly IServerConfiguration configuration;
        private readonly TcpSocketServer socketServer;
        private readonly IConnectionManager connectionManager;
        private readonly IConnectionReceiver connectionReceiver;

        public GameServer(IServerConfiguration serverConfiguration)
        {
            configuration = serverConfiguration;
            connectionReceiver = new ConnectionReceiver();
            connectionManager = new ConnectionManager(configuration.MaxConnections);
            socketServer = new TcpSocketServer(serverConfiguration, connectionManager, connectionReceiver);
        }

        public void Start()
        {
            socketServer.Start();
        }
    }
}
