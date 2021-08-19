using System;
using System.Numerics;
using Common.Base;
using Common.Model.Base;
using Common.Utility;

namespace Common.Extensions
{
    public static class CharacterExtensions
    {

        /// <summary>
        /// Turn to point
        /// </summary>
        /// <param name="model">center model</param>
        /// <param name="point">point turning towards</param>
        /// <returns></returns>
        public static BaseCharacterModel TurnToPoint(
            this BaseCharacterModel model,
            Vector3 point)
        {
            model.MovementType = MovementUtility.GetDirectionToPoint(model.Coordinates, point);
            return model;
        }

        /// <summary>
        /// Move to point
        /// </summary>
        /// <param name="model">center model</param>
        /// <param name="destination"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        public static BaseCharacterModel MoveToPoint(
            this BaseCharacterModel model,
            Vector3 destination,
            float speed)
        {
            var increment = GetCoordinatesToPoint(model.Coordinates, destination, speed);
            model.Coordinates = ClampCoordinatesToDestination(model.Coordinates, destination, increment);
            return model;
        }

        /// <summary>
        /// Move Coordinates of player
        /// </summary>
        /// <param name="model">Player model</param>
        /// <param name="speed">Speed of the movement</param>
        /// <param name="maxWidth">Max world width</param>
        /// <param name="maxHeight">Max world height</param>
        /// <returns></returns>
        public static BaseCharacterModel MoveCoordinates(
            this BaseCharacterModel model,
            float speed,
            int maxWidth,
            int maxHeight)
        {
            var coordinates = GetCoordinatesWithDirection(model, speed);
            model.Coordinates = ClampCoordinates(coordinates, maxWidth, maxHeight);
            return model;
        }

        /// <summary>
        /// Get coordinates in direction of point
        /// </summary>
        /// <param name="center"></param>
        /// <param name="point"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private static Vector3 GetCoordinatesToPoint(
            Vector3 center,
            Vector3 point,
            float speed)
        {
            var direction = point - center;
            return direction == Vector3.Zero
                ? Vector3.Zero
                : Vector3.Normalize(direction) * speed;
        }

        /// <summary>
        /// Get the coordinates with direction
        /// </summary>
        /// <param name="movementType"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private static Vector3 GetCoordinatesWithDirection(BaseCharacterModel model, float speed)
        {
            var coordinates = model.Coordinates;
            switch (model.MovementType)
            {
                case MovementType.UP:
                    coordinates.Y -= speed;
                    break;
                case MovementType.LEFT:
                    coordinates.X -= speed;
                    break;
                case MovementType.RIGHT:
                    coordinates.X += speed;
                    break;
                case MovementType.DOWN:
                    coordinates.Y += speed;
                    break;
                case MovementType.UP_LEFT:
                    coordinates.X -= speed;
                    coordinates.Y -= speed;
                    break;
                case MovementType.UP_RIGHT:
                    coordinates.X += speed;
                    coordinates.Y -= speed;
                    break;
                case MovementType.DOWN_LEFT:
                    coordinates.X -= speed;
                    coordinates.Y += speed;
                    break;
                case MovementType.DOWN_RIGHT:
                    coordinates.X += speed;
                    coordinates.Y += speed;
                    break;
                case MovementType.STOPPED:
                    break;
            }
            return coordinates;
        }

        private static Vector3 ClampCoordinatesToDestination(
            Vector3 coordinates,
            Vector3 destination,
            Vector3 increment)
        {
            var distance = Vector3.Abs(coordinates - destination);
            var absoluteIncrement = Vector3.Abs(increment);

            return distance.X < absoluteIncrement.X && distance.Y < absoluteIncrement.Y
                ? destination
                : coordinates += increment;
        }

        /// <summary>
        /// Clamps the coordinates to the world
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        private static Vector3 ClampCoordinates(Vector3 coordinates, int maxX, int maxY)
        {
            coordinates.X = Math.Clamp(coordinates.X, 0, maxX);
            coordinates.Y = Math.Clamp(coordinates.Y, 0, maxY);
            return coordinates;
        }
    }
}
