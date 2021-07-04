using System;
using System.Threading;
using System.Threading.Tasks;
using Server.Engine;
using Server.Network.Connection;
using Server.Network.Server;

namespace Sever
{
    public class GameServer
    {
        private readonly IServer _socketServer;
        private readonly IGameLoop _gameLoop;
        private readonly IConnectionDispatch _connectionDispatch;
        private readonly IConnectionReceiver _connectionReceiver;

        public GameServer(
            IServer socketServer,
            IGameLoop gameLoop,
            IConnectionDispatch connectionDispatch,
            IConnectionReceiver connectionReceiver)
        {
            _socketServer = socketServer;
            _gameLoop = gameLoop;
            _connectionReceiver = connectionReceiver;
            _connectionDispatch = connectionDispatch;
        }

        public void Start()
        {
            Console.WriteLine("Starting Game Server");
            _socketServer.Start();

            Task.Run(() =>
            {
                _gameLoop.Start();
            });
        }

        public void Close()
        {
            Console.WriteLine("Closing Game Server");
            _socketServer.Close();
            _gameLoop.Stop();
            _connectionDispatch.Close();
            _connectionReceiver.Close();
        }
    }
}
