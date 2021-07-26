using System;
using Common.Bus;
using Server.Bus.Game;
using Server.Bus.Packet;

namespace Server.Engine.Enemy
{
    public class EnemyManager : IEnemyManager, IEventBusListener<GameLoopEvent>
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IGameLoopBus _gameLoopBus;
        private readonly IEnemyStore _enemyStore;

        public EnemyManager(
            IDispatchPacketBus dispatchPacketBus,
            IGameLoopBus gameLoopBus,
            IEnemyStore enemyStore)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _enemyStore = enemyStore;
            _gameLoopBus = gameLoopBus;
            _gameLoopBus.Subscribe(this);
        }

        public void Handle(GameLoopEvent eventObject)
        {
            Update(eventObject.ElapsedTime);
        }

        private void Update(double elapsedTime)
        {

        }

        private void SpawnEnemy(double elapsedTime)
        {
            foreach(var enemy in _enemyStore.GetAll().Values)
            {
            }
        }
    }
}
