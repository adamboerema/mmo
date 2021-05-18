using System;
using Common.Network.Server.Configuration;
using Common.Network.Server.Socket;

namespace Common.Network.Server
{
    public class GameServer
    {
        private readonly IServerConfiguration configuration;
        private readonly TcpSocketServer socketServer;

        public GameServer(IServerConfiguration serverConfiguration)
        {
            configuration = serverConfiguration;
            socketServer = new TcpSocketServer(serverConfiguration.Port);
        }

        public void Start()
        {
            socketServer.Start();
        }
    }
}
