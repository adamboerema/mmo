using System;
using System.Drawing;
using Common.Bus;
using Server.Bus.Connection;
using Common.Model.Behavior;
using Common.Model.Shared;
using Common.Model.Character;
using Common.Utility;
using Server.Component.Enemy;
using Server.Network.Dispatch;
using Common.Store;
using Server.Component.Player;

namespace Server.Engine.Enemy
{
    public class EnemyManager : IEnemyManager, IEventBusListener<ConnectionEvent>
    {
        private readonly IConnectionBus _connectionBus;
        private readonly IEnemyDispatch _enemyDispatch;
        private readonly ComponentStore<EnemyComponent> _enemyStore;
        private readonly ComponentStore<PlayerComponent> _playerStore;

        public EnemyManager(
            IConnectionBus connectionBus,
            IEnemyDispatch enemyDispatch,
            ComponentStore<EnemyComponent> enemyStore,
            ComponentStore<PlayerComponent> playerStore)
        {
            _connectionBus = connectionBus;
            _enemyDispatch = enemyDispatch;
            _enemyStore = enemyStore;
            _playerStore = playerStore;
            _connectionBus.Subscribe(this);
            InitializeEnemies();
        }
            
        public void Handle(ConnectionEvent eventObject)
        {
            if(eventObject.State == ConnectionState.CONNECT)
            {
                DispatchEnemiesToPlayer(eventObject.Id);
            }
        }

        /// <summary>
        /// Update Loop
        /// </summary>
        /// <param name="elapsedTime"></param>
        /// <param name="timestamp"></param>
        public void Update(GameTick gameTick)
        {
            foreach(var enemy in _enemyStore.GetAll().Values)
            {
                enemy.Update(gameTick, WorldUtility.GetWorld());
            }
        }

        /// <summary>
        /// Dispatch all enemies to a player
        /// </summary>
        /// <param name="playerId"></param>
        public void DispatchEnemiesToPlayer(string playerId)
        {
            foreach (var enemy in _enemyStore.GetAll().Values)
            {
                enemy.DispatchEnemyToPlayer(playerId);
            }
        }

        /// <summary>
        /// Initialize enemies
        /// </summary>
        private void InitializeEnemies()
        {
            for (var i = 0; i < 3; i++)
            {
                var enemy = CreateEnemy();
                _enemyStore.Add(enemy);
            }
        }

        /// <summary>
        /// Create Enemy model
        /// </summary>
        /// <returns></returns>
        private EnemyComponent CreateEnemy()
        {
            return new EnemyComponent(
                new EnemyConfiguration
                {
                    Id = Guid.NewGuid().ToString(),
                    Type = EnemyType.TEST,
                    Spawn = new SpawnModel
                    {
                        IsAlive = true,
                        SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                        SpawnArea = new Rectangle(100, 100, 100, 100),
                        RespawnSeconds = 10
                    },
                    Pathing = new PathingModel
                    {
                        MovementWaitSeconds = 10,
                        MovementArea = new Rectangle(0, 100, 300, 300),
                        EngageDistance = 100,
                        EngageTargetId = null
                    },
                    Character = new CharacterModel
                    {
                        Name = "Test"
                    },
                    Collision = new CollisionModel
                    {
                        Bounds = new Bounds(10, 10)
                    },
                    Movement = new MovementModel
                    {
                        MovementSpeed = 0.2f,
                        Direction = Direction.DOWN,
                        IsMoving = false
                    },
                    Combat = new CombatModel
                    {
                        AttackRange = 10,
                        AttackSpeed = 1
                    }
                },
                _enemyDispatch,
                _playerStore);
        }
    }
}
