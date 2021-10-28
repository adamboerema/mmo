using System;
using System.Diagnostics;
using Common.Model.Shared;
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
        private readonly IEnemyManager _enemyComponent;
        private readonly IPlayerManager _playerComponent;

        public GameLoop(
            IServerConfiguration serverConfiguration,
            IPlayerManager playerComponent,
            IMovementComponent movementManager,
            IEnemyManager enemyManager)
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
                    
                    Update(new GameTick(elapsedTime, currentTime));
                    _gameLoopTimer.Restart();
                }
            }
        }

        public void Update(GameTick gameTick)
        {

            _movementComponent.Update(gameTick);
            _enemyComponent.Update(gameTick);
            _playerComponent.Update(gameTick);
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
