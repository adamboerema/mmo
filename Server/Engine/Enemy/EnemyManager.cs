using System;
using System.Drawing;
using System.Numerics;
using Common.Bus;
using Common.Base;
using Common.Packets.ServerToClient.Enemy;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Server.Engine.Player;
using Common.Utility;

namespace Server.Engine.Enemy
{
    public class EnemyManager : IEnemyManager, IEventBusListener<ConnectionEvent>
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IConnectionBus _connectionBus;
        private readonly IEnemyStore _enemyStore;
        private readonly IPlayerStore _playerStore;
        private readonly Random _random;

        public EnemyManager(
            IDispatchPacketBus dispatchPacketBus,
            IConnectionBus connectionBus,
            IEnemyStore enemyStore,
            IPlayerStore pLayerStore)
        {
            _random = new Random();
            _dispatchPacketBus = dispatchPacketBus;
            _connectionBus = connectionBus;
            _enemyStore = enemyStore;
            _playerStore = pLayerStore;
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
            foreach(var enemy in _enemyStore.GetAll().Values)
            {
                SpawnEnemy(timestamp, enemy);
                EngagePlayer(enemy);
                MoveEnemy(elapsedTime, timestamp, enemy);
                _enemyStore.Update(enemy);
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
        /// Engage player
        /// </summary>
        /// <param name="enemy"></param>
        private void EngagePlayer(EnemyModel enemy)
        {
            if(enemy.EngageTargetId == null)
            {
                foreach (var player in _playerStore.GetAll().Values)
                {
                    var absoluteDistance = MovementUtility.GetAbsoluteDistanceToPoint(
                        enemy.Coordinates,
                        player.Coordinates);

                    if (absoluteDistance < enemy.EngageDistance)
                    {
                        enemy.EngageCharacter(player);
                        DispatchEnemyEngage(enemy);
                    }
                }
            }

            if(enemy.EngageTargetId != null)
            {
                var player = _playerStore.Get(enemy.EngageTargetId);
                if (player != null)
                {
                    var distance = MovementUtility.GetAbsoluteDistanceToPoint(
                        enemy.Coordinates,
                        player.Coordinates);
                    var disengageDistance = distance * 2;

                    if (distance > disengageDistance)
                    {
                        enemy.DisengagePlayer();
                        DispatchEnemyDisenage(enemy);
                    }
                    else
                    {
                        enemy.MovementDestination = player.Coordinates;
                    }
                }
                else
                {
                    enemy.DisengagePlayer();
                    DispatchEnemyDisenage(enemy);
                }
            }
        }

        /// <summary>
        /// Trigger movement 
        /// </summary>
        /// <param name="elaspedTime"></param>
        /// <param name="timestamp"></param>
        /// <param name="enemy"></param>
        private void MoveEnemy(double elaspedTime, double timestamp, EnemyModel enemy)
        {
            var moveTime = enemy.LastMovementTime + enemy.MovementWaitSeconds;
            var shouldStartMove = moveTime < timestamp && enemy.EngageTargetId == null;

            if(shouldStartMove)
            {
                StartMovement(enemy);
            }

            var speed = (float)(enemy.MovementSpeed * elaspedTime);
            enemy.MoveToPoint(enemy.MovementDestination, speed);

            // Stop if moving and have reached location
            if(enemy.IsMoving && enemy.Coordinates == enemy.MovementDestination)
            {
                StopMovement(enemy);
            }
        }

        /// <summary>
        /// Turn and start the movement towards a point
        /// </summary>
        /// <param name="enemy"></param>
        private EnemyModel StartMovement(EnemyModel enemy)
        {
            var movementPoint = GetRandomWorldPoint(enemy.MovementArea);
            enemy.StartMovementTowardsPoint(movementPoint);
            DispatchEnemyMovement(enemy);
            return enemy;
        }

        /// <summary>
        /// Stop movement
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        private EnemyModel StopMovement(EnemyModel enemy)
        {
            enemy.StopMove();
            DispatchEnemyMovement(enemy);
            return enemy;
        }

        /// <summary>
        /// Find dead enemies and respawn if a certain time has past
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        private void SpawnEnemy(double timestamp, EnemyModel enemy)
        {
            var respawnTime = enemy.DeathTime + enemy.RespawnSeconds;
            var shouldRespawn = !enemy.IsAlive && respawnTime < timestamp;

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
            var randomWorldPoint = GetRandomWorldPoint(enemy.SpawnArea);
            enemy.Respawn(randomWorldPoint);
            _enemyStore.Add(enemy);
            DispatchEnemySpawn(enemy);
        }

        /// <summary>
        /// Gets a random spawn point within a rectangle spawn area
        /// </summary>
        /// <param name="spawnArea">Spawn area</param>
        /// <returns></returns>
        private Vector3 GetRandomWorldPoint(Rectangle spawnArea)
        {
            return new Vector3(
                _random.Next(spawnArea.Left, spawnArea.Right),
                _random.Next(spawnArea.Top, spawnArea.Bottom),
                0);
        }

        /// <summary>
        /// Create Enemy model
        /// </summary>
        /// <returns></returns>
        private EnemyModel CreateEnemy()
        {
            var spawnArea = new Rectangle(100, 100, 100, 100);
            var spawnPoint = GetRandomWorldPoint(spawnArea);
            var now = DateTimeOffset.Now.ToUnixTimeSeconds();

            return new EnemyModel
            {
                Id = Guid.NewGuid().ToString(),
                Type = EnemyType.TEST,
                SpawnTime = now,
                RespawnSeconds = 10,
                SpawnArea = spawnArea,
                MovementWaitSeconds = 10,
                MovementDestination = spawnPoint,
                LastMovementTime = now,
                MovementArea = new Rectangle(0, 100, 300, 300),
                EngageDistance = 100,
                EngageTargetId = null,
                Name = "Test",
                IsAlive = true,
                IsMoving = false,
                Coordinates = spawnPoint,
                Direction = Direction.DOWN,
                MovementSpeed = 0.2f
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
                Position = enemy.Coordinates,
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
                    TargetId = enemy.EngageTargetId,
                    Position = enemy.Coordinates,
                    MovementDestination = enemy.MovementDestination,
                    MovementSpeed = enemy.MovementSpeed,
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
                Position = enemy.Coordinates,
                MovementDestination = enemy.MovementDestination,
                MovementSpeed = enemy.MovementSpeed
            };
            _dispatchPacketBus.Publish(packet);
        }

        /// <summary>
        /// Dispatch enemy engagement
        /// </summary>
        /// <param name="enemy"></param>
        private void DispatchEnemyEngage(EnemyModel enemy)
        {
            var packet = new EnemyEngagePacket
            {
                EnemyId = enemy.Id,
                TargetId = enemy.EngageTargetId
            };
            _dispatchPacketBus.Publish(packet);
        }

        /// <summary>
        /// Dispatch enemy disengagement
        /// </summary>
        /// <param name="enemy"></param>
        private void DispatchEnemyDisenage(EnemyModel enemy)
        {
            var packet = new EnemyDisengagePacket
            {
                EnemyId = enemy.Id
            };
            _dispatchPacketBus.Publish(packet);
        }
    }
}
