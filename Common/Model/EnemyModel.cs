using System;
using System.Drawing;
using System.Numerics;
using Common.Model.Base;

namespace Common.Base
{
    public class EnemyModel: BaseCharacterModel
    {
        public EnemyType Type { get; init; }

        public int RespawnSeconds { get; init; }

        public Rectangle SpawnArea { get; init; }

        public double SpawnTime { get; private set; }

        public double DeathTime { get; private set; }


        /// <summary>
        /// Behavior
        /// </summary>
        public int EngageDistance { get; init; }

        public string EngageTargetId { get; private set; }

        /// <summary>
        /// Movement
        /// </summary>

        public Rectangle MovementArea { get; init; }

        public int MovementWaitSeconds { get; init; }

        public double LastMovementTime { get; private set; }

        public Vector3 MovementDestination { get; private set; }

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public EnemyModel EngageCharacter(BaseCharacterModel characterModel)
        {
            EngageTargetId = characterModel.Id;
            StartMovementTowardsPoint(characterModel.Coordinates);
            return this;
        }

        /// <summary>
        /// Starts movement towards point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public EnemyModel StartMovementTowardsPoint(Vector3 point)
        {
            LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            MovementDestination = point;
            TurnToPoint(point);
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
