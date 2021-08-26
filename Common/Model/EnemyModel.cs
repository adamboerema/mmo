using System;
using System.Drawing;
using System.Numerics;
using Common.Model.Base;
using Common.Utility;

namespace Common.Base
{
    public class EnemyModel: BaseCharacterModel
    {
        public EnemyType Type { get; init; }

        public int RespawnSeconds { get; init; } = 60;
        
        public Rectangle SpawnArea { get; init; }

        public double SpawnTime { get; private set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public double DeathTime { get; private set; }


        /// <summary>
        /// Behavior
        /// </summary>
        public int EngageDistance { get; init; } = 100;

        public string EngageTargetId { get; private set; }

        /// <summary>
        /// Movement
        /// </summary>

        public Rectangle MovementArea { get; init; }

        public int MovementWaitSeconds { get; init; } = 10;

        public double LastMovementTime { get; private set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public Vector3 MovementDestination { get; private set; }

        public EnemyModel(
            string name,
            EnemyType enemyType,
            Vector3 spawnPoint,
            Rectangle spawnArea,
            int respawnTime,
            float movementSpeed,
            int movementWaitSeconds,
            Rectangle movementArea
        )
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Type = enemyType;
            SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            Coordinates = spawnPoint;
            Direction = Direction.DOWN;
            IsAlive = true;
            IsMoving = false;
            RespawnSeconds = respawnTime;
            SpawnArea = spawnArea;
            MovementWaitSeconds = movementWaitSeconds;
            MovementDestination = spawnPoint;
            LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            MovementArea = movementArea;
            EngageDistance = 100;
            EngageTargetId = null;
            MovementSpeed = movementSpeed;
        }

        public EnemyModel(
            string id,
            string name,
            EnemyType enemyType,
            string engageTargetId,
            Vector3 coordinates,
            Vector3 movementDestination,
            float movementSpeed,
            bool isAlive)
        {
            Id = id;
            Name = name;
            Type = enemyType;
            EngageTargetId = engageTargetId;
            Coordinates = coordinates;
            MovementDestination = movementDestination;
            MovementSpeed = movementSpeed;
            IsAlive = isAlive;
        }

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EnemyModel EngageCharacter(string id, Vector3 position)
        {
            EngageTargetId = id;
            PathToPoint(Coordinates, position, MovementSpeed);
            return this;
        }

        /// <summary>
        /// Update the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public EnemyModel UpdateDestination(Vector3 destination)
        {
            MovementDestination = destination;
            return this;
        }
        
        /// <summary>
        /// Path to destintation
        /// </summary>
        /// <param name="toPoint"></param>
        /// <returns></returns>
        public EnemyModel PathToPoint(Vector3 toPoint)
        {
            PathToPoint(Coordinates, toPoint, MovementSpeed);
            return this;
        }

        /// <summary>
        /// Starts movement towards point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public EnemyModel PathToPoint(
            Vector3 fromPoint,
            Vector3 toPoint,
            float movementSpeed)
        {
            LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            Coordinates = fromPoint;
            MovementDestination = toPoint;
            MovementSpeed = movementSpeed;
            Direction = MovementUtility.GetDirectionToPoint(Coordinates, toPoint);
            return this;
        }

        /// <summary>
        /// Respawn the enemy
        /// </summary>
        /// <param name="respawnCoordinates"></param>
        /// <returns></returns>
        public EnemyModel Respawn(Vector3 respawnCoordinates)
        {
            SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            Coordinates = respawnCoordinates;
            IsAlive = true;
            IsMoving = false;
            return this;
        }

        /// <summary>
        /// Disengage the target
        /// </summary>
        /// <returns></returns>
        public EnemyModel DisengagePlayer()
        {
            EngageTargetId = null;
            MovementDestination = Coordinates;
            return this;
        }
    }
}
