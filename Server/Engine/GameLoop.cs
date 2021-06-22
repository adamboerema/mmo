using System;
using System.Diagnostics;
using Server.Configuration;
using Server.Network.Connection;

namespace Server.Engine
{
    public class GameLoop: IGameLoop
    {
        private bool IsRunning = false;
        private Stopwatch _gameLoopTimer = new Stopwatch();

        private readonly IServerConfiguration _serverConfiguration;

        public GameLoop(
            IServerConfiguration serverConfiguration)
        {
            _serverConfiguration = serverConfiguration;
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
                }
            }
        }

        public void Update(double elapsedTime)
        {

        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
