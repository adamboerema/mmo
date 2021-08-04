using System;
using System.Diagnostics;
using Server.Configuration;
using Server.Engine.Enemy;
using Server.Engine.Movement;
using Server.Engine.Player;

namespace Server.Engine
{
    public class GameLoop: IGameLoop
    {
        private bool IsRunning = false;
        private Stopwatch _gameLoopTimer = new Stopwatch();

        private readonly IServerConfiguration _serverConfiguration;
        private readonly IMovementManager _movementManager;
        private readonly IEnemyManager _enemyManager;

        public GameLoop(
            IServerConfiguration serverConfiguration,
            IMovementManager movementManager,
            IEnemyManager enemyManager)
        {
            _serverConfiguration = serverConfiguration;
            _movementManager = movementManager;
            _enemyManager = enemyManager;
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
                    var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                    Update(elapsedTime, currentTime);
                    _gameLoopTimer.Restart();
                }
            }
        }

        public void Update(double elapsedTime, double currentTime)
        {
            _movementManager.Update(elapsedTime, currentTime);
            _enemyManager.Update(elapsedTime, currentTime);
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
