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
        private readonly IMovementComponent _movementComponent;
        private readonly IEnemyComponent _enemyComponent;
        private readonly IPlayerComponent _playerComponent;

        public GameLoop(
            IServerConfiguration serverConfiguration,
            IPlayerComponent playerComponent,
            IMovementComponent movementManager,
            IEnemyComponent enemyManager)
        {
            _serverConfiguration = serverConfiguration;
            _playerComponent = playerComponent;
            _movementComponent = movementManager;
            _enemyComponent = enemyManager;
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
            _movementComponent.Update(elapsedTime, currentTime);
            _enemyComponent.Update(elapsedTime, currentTime);
            _playerComponent.Update(elapsedTime, currentTime);
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
