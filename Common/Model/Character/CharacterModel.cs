using System;
using System.Drawing;
using System.Numerics;
using Common.Base;
using Common.Utility;

namespace Common.Model.Character
{
    public class CharacterModel
    {
        public string Name { get; init; }

        public bool IsMoving { get; set; } = false;

        public float MovementSpeed { get; set; } = 0.2f;

        public Direction Direction { get; set; } = Direction.DOWN;

        public Vector3 Coordinates { get; set; }

        public Bounds Bounds { get; set; }

        /// <summary>
        /// Turn to point
        /// </summary>
        /// <param name="model">center model</param>
        /// <param name="point">point turning towards</param>
        /// <returns></returns>
        public void TurnToPoint(Vector3 point)
        {
            Direction = MovementUtility.GetDirectionToPoint(Coordinates, point);
        }

        /// <summary>
        /// Adjust the speed of the character
        /// </summary>
        /// <param name="movementSpeed"></param>
        /// <returns></returns>
        public void AdjustSpeed(float movementSpeed)
        {
            MovementSpeed = movementSpeed;
        }

        /// <summary>
        /// Move to point
        /// </summary>
        /// <param name="destination"></param>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        public void  MoveToPoint(
            Vector3 destination,
            double elapsedTime)
        {
            var speed = (float)(elapsedTime * MovementSpeed);
            var increment = GetCoordinatesToPoint(Coordinates, destination, speed);
            Coordinates = ClampCoordinatesToDestination(Coordinates, destination, increment);
            IsMoving = Coordinates != destination;
        }

        /// <summary>
        /// Move Coordinates of player
        /// </summary>
        /// <param name="model">Player model</param>
        /// <param name="speed">Speed of the movement</param>
        /// <param name="maxWidth">Max world width</param>
        /// <param name="maxHeight">Max world height</param>
        /// <returns></returns>
        public void Move(
            float speed,
            int maxWidth,
            int maxHeight)
        {
            if(IsMoving)
            {
                var coordinates = MoveInDirection(speed);
                Coordinates = ClampCoordinates(coordinates, maxWidth, maxHeight);
                IsMoving = true;
            }
        }

        /// <summary>
        /// Stop movement
        /// </summary>
        /// <returns></returns>
        public void StopMove()
        {
            IsMoving = false;
        }

        /// <summary>
        /// Get coordinates in direction of point
        /// </summary>
        /// <param name="center"></param>
        /// <param name="point"></param>
        /// <param name="speed"></param>
        /// <returns></returns>
        private Vector3 GetCoordinatesToPoint(
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
        /// <param name="speed"></param>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private Vector3 MoveInDirection(float speed)
        {
            if(!IsMoving)
            {
                return Coordinates;
            }

            var coordinates = Coordinates;
            switch (Direction)
            {
                case Direction.UP:
                    coordinates.Y -= speed;
                    break;
                case Direction.LEFT:
                    coordinates.X -= speed;
                    break;
                case Direction.RIGHT:
                    coordinates.X += speed;
                    break;
                case Direction.DOWN:
                    coordinates.Y += speed;
                    break;
                case Direction.UP_LEFT:
                    coordinates.X -= speed;
                    coordinates.Y -= speed;
                    break;
                case Direction.UP_RIGHT:
                    coordinates.X += speed;
                    coordinates.Y -= speed;
                    break;
                case Direction.DOWN_LEFT:
                    coordinates.X -= speed;
                    coordinates.Y += speed;
                    break;
                case Direction.DOWN_RIGHT:
                    coordinates.X += speed;
                    coordinates.Y += speed;
                    break;
            }
            return coordinates;
        }

        /// <summary>
        /// Clamps Coordinates to the destination point
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="destination"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        private Vector3 ClampCoordinatesToDestination(
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
        private Vector3 ClampCoordinates(Vector3 coordinates, int maxX, int maxY)
        {
            coordinates.X = Math.Clamp(coordinates.X, 0, maxX);
            coordinates.Y = Math.Clamp(coordinates.Y, 0, maxY);
            return coordinates;
        }
    }
}
