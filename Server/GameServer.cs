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
