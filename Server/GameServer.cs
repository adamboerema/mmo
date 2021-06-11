using System;
using Server.Network.Connection;
using Server.Network.Server;

namespace Sever
{
    public class GameServer
    {
        private readonly IServer _socketServer;
        private readonly IConnectionReceiver _connectionReceiver;
        private readonly IConnectionDispatch _connectionDispatch;

        public GameServer(
            IServer socketServer,
            IConnectionReceiver connectionReceiver,
            IConnectionDispatch connectionDispatch)
        {
            _socketServer = socketServer;
        }

        public void Start()
        {
            _socketServer.Start();
        }

        public void Close()
        {

        }
    }
}
