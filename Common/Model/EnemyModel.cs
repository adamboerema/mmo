using System;
using System.Numerics;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Utility;

namespace Common.Base
{
    public class EnemyModel
    {
        public string Id { get; init; }

        public EnemyType Type { get; init; }

        public SpawnModel Spawn { get; init; }

        public MovementModel Movement { get; init; }

        public CharacterModel Character { get; init; }

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EnemyModel EngageCharacter(string id, Vector3 position)
        {
            Movement.EngageTargetId = id;
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
            Movement.MovementDestination = destination;
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
            Movement.LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            Character.Coordinates = fromPoint;
            Movement.MovementDestination = toPoint;
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
            Spawn.SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
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
            Movement.EngageTargetId = null;
            Movement.MovementDestination = Character.Coordinates;
            return this;
        }
    }
}
