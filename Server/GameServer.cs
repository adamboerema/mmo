using System;
using Server.Auth;
using Server.Network.Connection;
using Server.Network.Server;

namespace Sever
{
    public class GameServer
    {
        private readonly IServer _socketServer;
        private readonly IConnectionDispatch _connectionDispatch;
        private readonly IConnectionReceiver _connectionReceiver;

        public GameServer(
            IServer socketServer,
            IConnectionDispatch connectionDispatch,
            IConnectionReceiver connectionReceiver)
        {
            _socketServer = socketServer;
            _connectionReceiver = connectionReceiver;
            _connectionDispatch = connectionDispatch;
        }

        public void Start()
        {
            Console.WriteLine("Starting Game Server");
            _socketServer.Start();
        }

        public void Close()
        {
            Console.WriteLine("Closing Game Server");
            _socketServer.Close();
            _connectionDispatch.Close();
            _connectionReceiver.Close();
        }
    }
}
