using System;
using System.Drawing;
using System.Numerics;

namespace Common.Model.Behavior
{
    public class BehaviorMovementModel
    {
        /// <summary>
        /// Behavior
        /// </summary>
        public int EngageDistance { get; set; } = 100;

        public string EngageTargetId { get; set; }

        /// <summary>
        /// Movement
        /// </summary>

        public Rectangle MovementArea { get; set; }

        public int MovementWaitSeconds { get; set; } = 10;

        public double LastMovementTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public Vector3 MovementDestination { get; set; }
    }
}
