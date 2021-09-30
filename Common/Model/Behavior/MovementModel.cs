using System;
using System.Drawing;
using System.Numerics;

namespace Common.Model.Behavior
{
    public class MovementModel
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

        /// <summary>
        /// Gets a random spawn point within a rectangle spawn area
        /// </summary>
        /// <param name="spawnArea">Spawn area</param>
        /// <returns></returns>
        public Vector3 GetRandomMovementPoint()
        {
            var random = new Random();
            return new Vector3(
                random.Next(MovementArea.Left, MovementArea.Right),
                random.Next(MovementArea.Top, MovementArea.Bottom),
                0);
        }
    }
}
