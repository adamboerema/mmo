using System;
using Server.Bus.Packet;
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
        private readonly IConnectionDispatch connectionDispatch;
        private readonly IConnectionReceiver connectionReceiver;

        private readonly PacketBus receiverBus = new PacketBus();
        private readonly PacketBus dispatchBus = new PacketBus();

        public GameServer(IServerConfiguration serverConfiguration)
        {
            configuration = serverConfiguration;
            connectionReceiver = new ConnectionReceiver();
            connectionManager = new ConnectionManager(configuration.MaxConnections);
            socketServer = new TcpSocketServer(serverConfiguration, connectionManager, receiverBus);
        }

        public void Start()
        {
            socketServer.Start();
        }
    }
}
