using System;
using System.Diagnostics;
using Common.Model.Shared;
using Server.Configuration;
using Server.Engine.Enemy;
using Server.Engine.Player;

namespace Server.Engine
{
    public class GameLoop: IGameLoop
    {
        private bool IsRunning = false;
        private Stopwatch _gameLoopTimer = new Stopwatch();

        private readonly IServerConfiguration _serverConfiguration;
        private readonly IEnemyManager _enemyManager;
        private readonly IPlayerManager _playerManager;

        public GameLoop(
            IServerConfiguration serverConfiguration,
            IPlayerManager playerManager,
            IEnemyManager enemyManager)
        {
            _serverConfiguration = serverConfiguration;
            _playerManager = playerManager;
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
                    
                    Update(new GameTick(elapsedTime, currentTime));
                    _gameLoopTimer.Restart();
                }
            }
        }

        public void Update(GameTick gameTick)
        {

            _enemyManager.Update(gameTick);
            _playerManager.Update(gameTick);
        }

        public void Stop()
        {
            IsRunning = false;
        }
    }
}
