using System;
using Common.Network.Client.Configuration;

namespace Common.Network.Client
{
    public class GameClient
    {
        private readonly IGameConfiguration configuration;
        private readonly TcpSocketClient client;

        public GameClient(IGameConfiguration gameConfiguration)
        {
            configuration = gameConfiguration;
            client = new TcpSocketClient(gameConfiguration.IpAddress, gameConfiguration.Port);
        }

        public void Start()
        {
            client.Start();
        }
    }
}
