using System;
using System.Drawing;
using System.Numerics;
using Common.Model.Base;

namespace Common.Base
{
    public class EnemyModel: BaseCharacterModel
    {
        public EnemyType Type { get; set; }

        public double SpawnTime { get; set; }

        public double DeathTime { get; set; }

        public int RespawnSeconds { get; set; }

        public Rectangle SpawnArea { get; set; }

        /// <summary>
        /// Behavior
        /// </summary>
        public int EngageDistance { get; set; }

        public string EngageTargetId { get; set; }

        /// <summary>
        /// Movement
        /// </summary>
        public double LastMovementTime { get; set; }

        public int MovementWaitSeconds { get; set; }

        public Vector3 MovementDestination { get; set; }

        public Rectangle MovementArea { get; set; }

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
