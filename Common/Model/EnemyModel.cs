using System;
using System.Numerics;
using Common.Model;
using Common.Model.Behavior;
using Common.Utility;

namespace Common.Base
{
    public class EnemyModel
    {
        public string Id { get; init; }

        public EnemyType Type { get; init; }

        public BehaviorSpawnModel BehaviorSpawn { get; init; }

        public BehaviorMovementModel BehaviorMovement { get; init; }

        public CharacterModel Character { get; init; } 

        //public EnemyModel(
        //    string name,
        //    EnemyType enemyType,
        //    Vector3 spawnPoint,
        //    Rectangle spawnArea,
        //    int respawnTime,
        //    float movementSpeed,
        //    int movementWaitSeconds,
        //    Rectangle movementArea
        //)
        //{
        //    Id = Guid.NewGuid().ToString();
        //    Name = name;
        //    Type = enemyType;
        //    SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        //    Coordinates = spawnPoint;
        //    Direction = Direction.DOWN;
        //    IsAlive = true;
        //    IsMoving = false;
        //    RespawnSeconds = respawnTime;
        //    SpawnArea = spawnArea;
        //    MovementWaitSeconds = movementWaitSeconds;
        //    MovementDestination = spawnPoint;
        //    LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        //    MovementArea = movementArea;
        //    EngageDistance = 100;
        //    EngageTargetId = null;
        //    MovementSpeed = movementSpeed;
        //}

        //public EnemyModel(
        //    string id,
        //    string name,
        //    EnemyType enemyType,
        //    string engageTargetId,
        //    Vector3 coordinates,
        //    Vector3 movementDestination,
        //    float movementSpeed,
        //    bool isAlive)
        //{
        //    Id = id;
        //    Name = name;
        //    Type = enemyType;
        //    EngageTargetId = engageTargetId;
        //    Coordinates = coordinates;
        //    MovementDestination = movementDestination;
        //    MovementSpeed = movementSpeed;
        //    IsAlive = isAlive;
        //}

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EnemyModel EngageCharacter(string id, Vector3 position)
        {
            BehaviorMovement.EngageTargetId = id;
            PathToPoint(
                Character.Coordinates,
                position,
                Character.MovementSpeed);
            return this;
        }

        /// <summary>
        /// Update the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public EnemyModel UpdateDestination(Vector3 destination)
        {
            BehaviorMovement.MovementDestination = destination;
            return this;
        }
        
        /// <summary>
        /// Path to destintation
        /// </summary>
        /// <param name="toPoint"></param>
        /// <returns></returns>
        public EnemyModel PathToPoint(Vector3 toPoint)
        {
            PathToPoint(
                Character.Coordinates,
                toPoint,
                Character.MovementSpeed);
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
            BehaviorMovement.LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            Character.Coordinates = fromPoint;
            BehaviorMovement.MovementDestination = toPoint;
            Character.MovementSpeed = movementSpeed;
            Character.Direction = MovementUtility.GetDirectionToPoint(Character.Coordinates, toPoint);
            return this;
        }

        /// <summary>
        /// Respawn the enemy
        /// </summary>
        /// <param name="respawnCoordinates"></param>
        /// <returns></returns>
        public EnemyModel Respawn(Vector3 respawnCoordinates)
        {
            BehaviorSpawn.SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            Character.Coordinates = respawnCoordinates;
            Character.IsAlive = true;
            Character.IsMoving = false;
            return this;
        }

        /// <summary>
        /// Disengage the target
        /// </summary>
        /// <returns></returns>
        public EnemyModel DisengagePlayer()
        {
            BehaviorMovement.EngageTargetId = null;
            BehaviorMovement.MovementDestination = Character.Coordinates;
            return this;
        }
    }
}
