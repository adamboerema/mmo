using System;
using System.Drawing;
using System.Numerics;
using Common.Bus;
using Common.Model;
using Common.Packets.ServerToClient.Enemy;
using Server.Bus.Connection;
using Server.Bus.Packet;

namespace Server.Engine.Enemy
{
    public class EnemyManager : IEnemyManager, IEventBusListener<ConnectionEvent>
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IConnectionBus _connectionBus;
        private readonly IEnemyStore _enemyStore;
        private readonly Random _random;

        public EnemyManager(
            IDispatchPacketBus dispatchPacketBus,
            IConnectionBus connectionBus,
            IEnemyStore enemyStore)
        {
            _random = new Random();
            _dispatchPacketBus = dispatchPacketBus;
            _connectionBus = connectionBus;
            _enemyStore = enemyStore;
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
        public void Update(double elapsedTime, double timestamp)
        {
            foreach (var enemy in _enemyStore.GetAll().Values)
            {
                SpawnEnemy(timestamp, enemy);
                MoveEnemy(timestamp, enemy);
            }
        }

        /// <summary>
        /// Initialize enemies
        /// </summary>
        private void InitializeEnemies()
        {
            for (var i = 0; i < 10; i++)
            {
                var enemy = CreateEnemy();
                _enemyStore.Add(enemy);
                DispatchEnemySpawn(enemy);
            }
        }


        /// <summary>
        /// Trigger enemy movement
        /// </summary>
        /// <param name="timestamp"></param>
        private void MoveEnemy(double timestamp, EnemyModel enemy)
        {
            var moveTime = enemy.LastMovementTime + enemy.MovementSeconds;
            var shouldMove = moveTime < timestamp;
            if(shouldMove)
            {
                enemy.LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            }
            
        }

        /// <summary>
        /// Find dead enemies and respawn if a certain time has past
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        private void SpawnEnemy(double timestamp, EnemyModel enemy)
        {
            var respawnTime = enemy.DeathTime + enemy.RespawnSeconds;
            var shouldRespawn = !enemy.Character.IsAlive && respawnTime < timestamp;

            if (shouldRespawn)
            {
                RespawnEnemy(enemy);
            }
        }

        /// <summary>
        /// Will reset timers and respawn enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void RespawnEnemy(EnemyModel enemy)
        {
            enemy.SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            enemy.Character.IsAlive = true;
            enemy.Character.Coordinates = GetRandomSpawnPoint(enemy.SpawnArea);
            enemy.Character.MovementType = MovementType.STOPPED;
            _enemyStore.Update(enemy);
            DispatchEnemySpawn(enemy);
        }

        /// <summary>
        /// Gets a random spawn point within a rectangle spawn area
        /// </summary>
        /// <param name="rectangle">Spawn area</param>
        /// <returns></returns>
        private Vector3 GetRandomSpawnPoint(Rectangle rectangle)
        {
            return new Vector3(
                _random.Next(rectangle.Left, rectangle.Right),
                _random.Next(rectangle.Top, rectangle.Bottom),
                0);
        }

        private EnemyModel CreateEnemy()
        {
            return new EnemyModel
            {
                Id = Guid.NewGuid().ToString(),
                Type = EnemyType.TEST,
                SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                RespawnSeconds = 10,
                MovementSeconds = 10,
                SpawnArea = new Rectangle(100, 100, 100, 100),
                MovementArea = new Rectangle(100, 100, 100, 100),
                Character = new CharacterModel
                {
                    Name = "Test",
                    IsAlive = true,
                    MovementSpeed = 0.5f
                }
            }; 
        }

        /// <summary>
        /// Dispatch when an enemy spawns
        /// </summary>
        /// <param name="enemy"></param>
        private void DispatchEnemySpawn(EnemyModel enemy)
        {
            var packet = new EnemySpawnPacket
            {
                EnemyId = enemy.Id,
                Type = enemy.Type,
                Position = enemy.Character.Coordinates,
            };
            _dispatchPacketBus.Publish(packet);
        }

        /// <summary>
        /// Dispatch all enemies to player
        /// </summary>
        /// <param name="playerId"></param>
        private void DispatchEnemiesToPlayer(string playerId)
        {
            foreach (var enemy in _enemyStore.GetAll().Values)
            {
                var packet = new EnemySpawnPacket
                {
                    EnemyId = enemy.Id,
                    Type = enemy.Type,
                    Position = enemy.Character.Coordinates,
                };
                _dispatchPacketBus.Publish(playerId, packet);
            }
        }

        /// <summary>
        /// Dispatch when an enemy moves
        /// </summary>
        /// <param name="enemy"></param>
        private void DispatchEnemyMovement(EnemyModel enemy)
        {
            var packet = new EnemyMovementPacket
            {
                EnemyId = enemy.Id,
                Position = enemy.Character.Coordinates,
                MovementType = enemy.Character.MovementType,
                MovementSpeed = enemy.Character.MovementSpeed
            };
            _dispatchPacketBus.Publish(packet);
        }
    }
}
