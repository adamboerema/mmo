﻿using System;
using System.Drawing;
using System.Numerics;
using Common.Bus;
using Common.Model;
using Common.Packets.ServerToClient.Enemy;
using Server.Bus.Connection;
using Server.Bus.Packet;
using Common.Extensions;
using Common.Utility;

namespace Server.Engine.Enemy
{
    public class EnemyManager : IEnemyManager, IEventBusListener<ConnectionEvent>
    {
        private const int MAX_WIDTH = 1000;
        private const int MAX_HEIGHT = 1000;

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
                MoveEnemy(elapsedTime, timestamp, enemy);
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
        /// Trigger movement 
        /// </summary>
        /// <param name="elaspedTime"></param>
        /// <param name="timestamp"></param>
        /// <param name="enemy"></param>
        private void MoveEnemy(double elaspedTime, double timestamp, EnemyModel enemy)
        {
            var previousDirection = enemy.Character.MovementType;
            var moveTime = enemy.LastMovementTime + enemy.MovementWaitSeconds;
            var shouldStartMove = moveTime < timestamp;

            if(shouldStartMove)
            {
                enemy.LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
                enemy.MovementDestination = GetRandomWorldPoint(enemy.MovementArea);
            }

            var speed = (float)(enemy.Character.MovementSpeed * elaspedTime);
            enemy.Character.MoveToPoint(speed, enemy.MovementDestination);

            if (enemy.Character.MovementType != previousDirection)
            {
                DispatchEnemyMovement(enemy);
            }
            _enemyStore.Update(enemy);
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
            enemy.Character.Coordinates = GetRandomWorldPoint(enemy.SpawnArea);
            enemy.Character.MovementType = MovementType.STOPPED;
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
                MovementArea = new Rectangle(0, 100, 100, 100),
                Character = new CharacterModel
                {
                    Name = "Test",
                    IsAlive = true,
                    Coordinates = spawnPoint,
                    MovementType = MovementType.STOPPED,
                    MovementSpeed = 0.2f
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
