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
using Common.Model;
using Common.Model.Behavior;
using Common.Model.Character;

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
            for (var i = 0; i < 3; i++)
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
            if(enemy.Movement.EngageTargetId == null)
            {
                foreach (var player in _playerStore.GetAll().Values)
                {
                    var absoluteDistance = MovementUtility.GetAbsoluteDistanceToPoint(
                        enemy.Character.Coordinates,
                        player.Coordinates);

                    if (absoluteDistance < enemy.Movement.EngageDistance)
                    {
                        enemy.EngageCharacter(player.Id, player.Coordinates);
                        DispatchEnemyEngage(enemy);
                    }
                }
            }

            if(enemy.Movement.EngageTargetId != null)
            {
                var player = _playerStore.Get(enemy.Movement.EngageTargetId);
                if (player != null)
                {
                    var distance = MovementUtility.GetAbsoluteDistanceToPoint(
                        enemy.Character.Coordinates,
                        player.Coordinates);
                    var disengageDistance = distance * 2;

                    if (distance > disengageDistance)
                    {
                        enemy.DisengagePlayer();
                        DispatchEnemyDisenage(enemy);
                    }
                    else
                    {
                        enemy.UpdateDestination(player.Coordinates);
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
            var moveTime = enemy.Movement.LastMovementTime + enemy.Movement.MovementWaitSeconds;
            var shouldStartMove = moveTime < timestamp && enemy.Movement.EngageTargetId == null;

            if(shouldStartMove)
            {
                StartMovement(enemy);
            }

            var speed = (float)(enemy.Character.MovementSpeed * elaspedTime);
            enemy.Character.MoveToPoint(enemy.Movement.MovementDestination, speed);

            // Stop if moving and have reached location
            if(enemy.Character.IsMoving
                && enemy.Character.Coordinates == enemy.Movement.MovementDestination)
            {
                StopMovement(enemy);
            }
        }

        /// <summary>
        /// Turn and start the movement towards a point
        /// </summary>
        /// <param name="enemy"></param>
        private void StartMovement(EnemyModel enemy)
        {
            var movementPoint = GetRandomWorldPoint(enemy.Movement.MovementArea);
            enemy.PathToPoint(movementPoint);
            DispatchEnemyMovement(enemy);
        }

        /// <summary>
        /// Stop movement
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        private void StopMovement(EnemyModel enemy)
        {
            enemy.Character.StopMove();
            DispatchEnemyMovement(enemy);
        }

        /// <summary>
        /// Find dead enemies and respawn if a certain time has past
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        private void SpawnEnemy(double timestamp, EnemyModel enemy)
        {
            var respawnTime = enemy.Spawn.DeathTime + enemy.Spawn.RespawnSeconds;
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
            var randomWorldPoint = GetRandomWorldPoint(enemy.Spawn.SpawnArea);
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

            return new EnemyModel
            {
                Id = Guid.NewGuid().ToString(),
                Type = EnemyType.TEST,
                Character = new CharacterModel
                {
                    Name = "Test",
                    Coordinates = spawnPoint,
                    MovementSpeed = 0.2f,
                    Direction = Direction.DOWN,
                    IsAlive = true,
                    IsMoving = false
                },
                Spawn = new SpawnModel
                {
                    SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                    SpawnArea = spawnArea,
                    RespawnSeconds = 10
                },
                Movement = new MovementModel
                {
                    MovementDestination = spawnPoint,
                    MovementWaitSeconds = 10,
                    MovementArea = new Rectangle(0, 100, 300, 300),
                    EngageDistance = 100,
                    EngageTargetId = null
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
                    TargetId = enemy.Movement.EngageTargetId,
                    Position = enemy.Character.Coordinates,
                    MovementDestination = enemy.Movement.MovementDestination,
                    MovementSpeed = enemy.Character.MovementSpeed,
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
                MovementDestination = enemy.Movement.MovementDestination,
                MovementSpeed = enemy.Character.MovementSpeed
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
                TargetId = enemy.Movement.EngageTargetId
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
