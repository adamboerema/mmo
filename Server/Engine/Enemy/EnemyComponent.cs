using System;
using System.Drawing;
using Common.Bus;
using Common.Packets.ServerToClient.Enemy;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Server.Engine.Player;
using Common.Model.Behavior;
using Common.Model.Shared;
using Common.Model.Character;
using Common.Entity;
using Common.Utility;

namespace Server.Engine.Enemy
{
    public class EnemyManager : IEnemyComponent, IEventBusListener<ConnectionEvent>
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IConnectionBus _connectionBus;
        private readonly IEnemyStore _enemyStore;
        private readonly IPlayerStore _playerStore;

        public EnemyManager(
            IDispatchPacketBus dispatchPacketBus,
            IConnectionBus connectionBus,
            IEnemyStore enemyStore,
            IPlayerStore pLayerStore)
        {
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
        public void Update(GameTick gameTime)
        {
            foreach(var enemy in _enemyStore.GetAll().Values)
            {
                SpawnEnemy(gameTime, enemy);
                EngagePlayer(enemy);
                MoveEnemy(gameTime, enemy);
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
        private void EngagePlayer(EnemyEntity enemy)
        {
            if(enemy.EngageTargetId == null)
            {
                CheckEnemyEngage(enemy);
            }
            else
            {
                CheckEnemyDisengage(enemy);
            }
        }

        /// <summary>
        /// Check if enemy should start engaging
        /// </summary>
        /// <param name="enemy"></param>
        private void CheckEnemyEngage(EnemyEntity enemy)
        {
            foreach (var player in _playerStore.GetAll().Values)
            {
                if (enemy.ShouldEngage(player.Coordinates))
                {
                    enemy.EngageTarget(player.Id, player.Coordinates);
                    DispatchEnemyEngage(enemy);
                }
            }
        }

        /// <summary>
        /// Check if enemy should disengage
        /// </summary>
        /// <param name="enemy"></param>
        private void CheckEnemyDisengage(EnemyEntity enemy)
        {
            var player = _playerStore.Get(enemy.EngageTargetId);
            if (player != null)
            {
                if (enemy.ShouldDisengage(player.Coordinates))
                {
                    enemy.DisengageCharacter();
                    DispatchEnemyDisenage(enemy);
                }
                else
                {
                    enemy.SetEngageDestination(player.Coordinates);
                }
            }
            else
            {
                enemy.DisengageCharacter();
                DispatchEnemyDisenage(enemy);
            }
        }

        /// <summary>
        /// Trigger movement 
        /// </summary>
        /// <param name="elaspedTime"></param>
        /// <param name="timestamp"></param>
        /// <param name="enemy"></param>
        private void MoveEnemy(GameTick gameTick, EnemyEntity enemy)
        {
            if(enemy.ShouldStartMove(gameTick))
            {
                StartMovement(enemy);
            }

            enemy.Update(gameTick, WorldUtility.GetWorld());

            // Stop if moving and have reached location
            if(enemy.ShouldStopMove())
            {
                StopMovement(enemy);
            }
        }

        /// <summary>
        /// Turn and start the movement towards a point
        /// </summary>
        /// <param name="enemy"></param>
        private void StartMovement(EnemyEntity enemy)
        {
            var movementPoint = enemy.GetRandomMovementPoint();
            enemy.PathToPoint(movementPoint);
            DispatchEnemyMovement(enemy);
        }

        /// <summary>
        /// Stop movement
        /// </summary>
        /// <param name="enemy"></param>
        /// <returns></returns>
        private void StopMovement(EnemyEntity enemy)
        {
            enemy.StopMove();
            DispatchEnemyMovement(enemy);
        }

        /// <summary>
        /// Find dead enemies and respawn if a certain time has past
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        private void SpawnEnemy(GameTick gameTick, EnemyEntity enemy)
        {
            if (enemy.ShouldRespawn(gameTick))
            {
                RespawnEnemy(enemy);
            }
        }

        /// <summary>
        /// Will reset timers and respawn enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void RespawnEnemy(EnemyEntity enemy)
        {
            enemy.Respawn();
            _enemyStore.Add(enemy);
            DispatchEnemySpawn(enemy);
        }

        /// <summary>
        /// Create Enemy model
        /// </summary>
        /// <returns></returns>
        private EnemyEntity CreateEnemy()
        {
            var spawn = new SpawnModel
            {
                IsAlive = true,
                SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds(),
                SpawnArea = new Rectangle(100, 100, 100, 100),
                RespawnSeconds = 10
            };
            var spawnPoint = spawn.GetRandomSpawnPoint();

            return new EnemyEntity(
                id: Guid.NewGuid().ToString(),
                type: EnemyType.TEST,
                spawnModel: spawn,
                pathingModel: new PathingModel
                {
                    MovementDestination = spawnPoint,
                    MovementWaitSeconds = 10,
                    MovementArea = new Rectangle(0, 100, 300, 300),
                    EngageDistance = 100,
                    EngageTargetId = null
                },
                characterModel: new CharacterModel
                {
                    Name = "Test"
                },
                collisionModel: new CollisionModel
                {
                    Bounds = new Bounds(10, 10)
                },
                movementModel: new MovementModel
                {
                    Coordinates = spawnPoint,
                    MovementSpeed = 0.2f,
                    Direction = Direction.DOWN,
                    IsMoving = false
                },
                combatModel: new CombatModel
                {
                    AttackRange = 10,
                    AttackSpeed = 1
                });
        }

        /// <summary>
        /// Dispatch when an enemy spawns
        /// </summary>
        /// <param name="enemy"></param>
        private void DispatchEnemySpawn(EnemyEntity enemy)
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
        private void DispatchEnemyMovement(EnemyEntity enemy)
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
        private void DispatchEnemyEngage(EnemyEntity enemy)
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
        private void DispatchEnemyDisenage(EnemyEntity enemy)
        {
            var packet = new EnemyDisengagePacket
            {
                EnemyId = enemy.Id
            };
            _dispatchPacketBus.Publish(packet);
        }
    }
}
