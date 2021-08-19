using System;
using System.Drawing;
using System.Numerics;
using Common.Model.Base;

namespace Common.Base
{
    public class EnemyModel: BaseCharacterModel
    {
        public string Id { get; set; }

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
    }
}
