using System;
using System.Numerics;
using Common.Base;

namespace Common.Utility
{
    public static class MovementUtility
    {

        /// <summary>
        /// Sets the movement type towards the point
        /// </summary>
        /// <param name="charater"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static MovementType GetDirectionToPoint(Vector3 center, Vector3 point)
        {
            var vectorAngle = (float) GetAngleFromCenter(center, point);
            return GetDirectionFromAngle(vectorAngle);
        }

        /// <summary>
        /// Gets absolute distance from center to point
        /// </summary>
        /// <param name="center"></param>
        /// <param name="point"></param>
        /// <returns></returns>
        public static float GetAbsoluteDistanceToPoint(Vector3 center, Vector3 point)
        {
            var difference = Vector3.Distance(center, point);
            return Math.Abs(difference);
        }

        /// <summary>
        /// Get direction from the angle degrees
        /// </summary>
        /// <param name="directionAngle"></param>
        /// <returns></returns>
        public static MovementType GetDirectionFromAngle(float directionAngle)
        {
            var movementType = MovementType.STOPPED;
            switch (directionAngle)
            {
                case float angle when (angle >= 337.5 && angle < 360 && angle >= 0 && angle < 22.5):
                    movementType = MovementType.UP;
                    break;
                case float angle when (angle >= 22.5 && angle < 67.5):
                    movementType = MovementType.UP_RIGHT;
                    break;
                case float angle when (angle >= 67.5 && angle < 112.5):
                    movementType = MovementType.RIGHT;
                    break;
                case float angle when (angle >= 112.5 && angle < 157.5):
                    movementType = MovementType.DOWN_RIGHT;
                    break;
                case float angle when (angle >= 157.5 && angle < 202.5):
                    movementType = MovementType.DOWN;
                    break;
                case float angle when (angle >= 202.5 && angle < 247.5):
                    movementType = MovementType.DOWN_LEFT;
                    break;
                case float angle when (angle >= 247.5 && angle < 292.5):
                    movementType = MovementType.LEFT;
                    break;
                case float angle when (angle >= 292.5 && angle < 337.5):
                    movementType = MovementType.UP_LEFT;
                    break;
            }
            return movementType;
        }

        /// <summary>
        /// Get angle from a center point to another outlier point
        /// </summary>
        /// <param name="center">Center vector</param>
        /// <param name="point">Point relative to center</param>
        /// <returns></returns>
        public static double GetAngleFromCenter(Vector3 center, Vector3 point)
        {
            Vector3 relativePoint = point - center;
            var radians = MathF.Atan2(relativePoint.Y, relativePoint.X);
            var degrees = radians * 180 / MathF.PI;
            return (degrees + 450) % 360f;
        }
    }
}
