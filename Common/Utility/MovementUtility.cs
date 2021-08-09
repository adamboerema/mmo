using System;
using System.Numerics;
using Common.Model;

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
            var movementType = MovementType.STOPPED;
            var vectorAngle = AngleFromPoints(center, point);

            switch (vectorAngle)
            {
                case double angle when (angle >= 337.5 && angle < 360 && angle >= 0 && angle < 22.5):
                    movementType = MovementType.UP;
                    break;
                case double angle when (angle >= 22.5 && angle < 67.5):
                    movementType = MovementType.UP_RIGHT;
                    break;
                case double angle when (angle >= 67.5 && angle < 112.5):
                    movementType = MovementType.RIGHT;
                    break;
                case double angle when (angle >= 112.5 && angle < 157.5):
                    movementType = MovementType.DOWN_RIGHT;
                    break;
                case double angle when (angle >= 157.5 && angle < 202.5):
                    movementType = MovementType.DOWN;
                    break;
                case double angle when (angle >= 202.5 && angle < 247.5):
                    movementType = MovementType.DOWN_LEFT;
                    break;
                case double angle when (angle >= 247.5 && angle < 292.5):
                    movementType = MovementType.LEFT;
                    break;
                case double angle when (angle >= 292.5 && angle < 337.5):
                    movementType = MovementType.UP_LEFT;
                    break;
            }
            return movementType;
        }

        /// <summary>
        /// AngleBetween - the angle between 2 vectors
        /// </summary>
        /// <returns>
        /// Returns the the angle in degrees between vector1 and vector2
        /// </returns>
        /// <param name="vector1"> The first Vector </param>
        /// <param name="vector2"> The second Vector </param>
        public static double AngleFromPoints(Vector3 vector1, Vector3 vector2)
        {
            double theta = Math.Atan2(vector2.Y - vector1.Y, vector2.X - vector1.X);
            double angle = (360 - ((theta * 180) / Math.PI)) % 360;

            return angle;
        }
    }
}
