using System;
using Server.Network.Server;

namespace Sever
{
    public class GameServer
    {
        private readonly IServer _socketServer;

        public GameServer(IServer socketServer)
        {
            _socketServer = socketServer;
        }

        public void Start()
        {
            _socketServer.Start();
        }
    }
}
