using System;
using System.Drawing;
using System.Numerics;
using Common.Utility;

namespace Common.Model.Behavior
{
    public class MovementModel
    {
        private const int DISENGAGE_WAIT_SECONDS = 5;
        private double _disengageOffset => LastDisengageTime + DISENGAGE_WAIT_SECONDS;

        public int EngageDistance { get; set; } = 100;

        public string EngageTargetId { get; set; }

        public double LastDisengageTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public Rectangle MovementArea { get; set; }

        public int MovementWaitSeconds { get; set; } = 10;

        public double LastMovementTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public Vector3 MovementDestination { get; set; }

        /// <summary>
        /// Is target within engage range
        /// </summary>
         /// <param name="currentPosition">Current position of entity</param>
        /// <param name="target">Target to check range on</param>
        /// <returns></returns>
        public bool ShouldEngage(Vector3 currentPosition, Vector3 target)
        {
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            var absoluteDistance = MovementUtility.GetAbsoluteDistanceToPoint(
                currentPosition,
                target);
            return absoluteDistance < EngageDistance
                && currentTime > _disengageOffset;
        }

        /// <summary>
        /// Is target outside the 
        /// </summary>
        /// <param name="currentPosition">Current position of entity</param>
        /// <param name="target"></param>
        /// <returns></returns>
        public bool ShouldDisengage(Vector3 currentPosition, Vector3 target)
        {
            var currentTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            var absoluteDistance = MovementUtility.GetAbsoluteDistanceToPoint(
                currentPosition,
                target);
            var disengageDistance = EngageDistance * 2;
            return absoluteDistance > disengageDistance
                && currentTime > _disengageOffset;

        }


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
