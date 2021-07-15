using System;
using System.Diagnostics;
using Server.Bus.Game;
using Server.Configuration;
using Server.Engine.Player;

namespace Server.Engine
{
    public class GameLoop: IGameLoop
    {
        private bool IsRunning = false;
        private Stopwatch _gameLoopTimer = new Stopwatch();
        private IGameLoopBus _gameLoopBus;

        private readonly IServerConfiguration _serverConfiguration;

        public GameLoop(
            IServerConfiguration serverConfiguration,
            IGameLoopBus gameLoopBus)
        {
            _serverConfiguration = serverConfiguration;
            _gameLoopBus = gameLoopBus;
        }

        public void Start()
        {
            IsRunning = true;
            _gameLoopTimer.Reset();
            _gameLoopTimer.Start();
            while (IsRunning)
            {
                var elapsedTime = _gameLoopTimer.Elapsed.TotalMilliseconds;
                if (elapsedTime > _serverConfiguration.ServerTickRate)
                {
                    Update(elapsedTime);
                    _gameLoopTimer.Restart();
                }
            }
        }

        public void Update(double elapsedTime)
        {
            _gameLoopBus.Publish(elapsedTime);
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
