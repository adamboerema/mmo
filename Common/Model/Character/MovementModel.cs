using System;
using System.Numerics;
using Common.Base;
using Common.Utility;

namespace Common.Model.Shared
{
    public class MovementModel
    {
        public bool IsMoving { get; set; } = false;

        public float MovementSpeed { get; set; } = 0.2f;

        public Direction Direction { get; set; } = Direction.DOWN;

        public Vector3 Coordinates { get; set; }

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
        public void MoveToPoint(
            Vector3 destination,
            double elapsedTime)
        {
            var increment = GetCoordinatesToPoint(Coordinates, destination, elapsedTime);

            Console.WriteLine($"Coordinates ({Coordinates.X} {Coordinates.Y}) --- Destination ({destination.X} {destination.Y})");

            ClampCoordinatesToDestination(destination, increment);
            IsMoving = Coordinates != destination;
        }

        /// <summary>
        /// Move Coordinates of player
        /// </summary>
        /// <param name="model">Player model</param>
        /// <param name="elapsedTime">Elapsed time</param>
        /// <param name="maxWidth">Max world width</param>
        /// <param name="maxHeight">Max world height</param>
        /// <returns></returns>
        public void Move(
            double elapsedTime,
            int maxWidth,
            int maxHeight)
        {
            if (IsMoving)
            {
                MoveInDirection(elapsedTime);
                ClampCoordinates(maxWidth, maxHeight);
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
        /// Get the coordinates with direction
        /// </summary>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        private void MoveInDirection(double elapsedTime)
        {
            var direction = Vector3.Zero;

            switch (Direction)
            {
                case Direction.UP:
                    direction += new Vector3(0, -1, 0);
                    break;
                case Direction.LEFT:
                    direction += new Vector3(-1, 0, 0);
                    break;
                case Direction.RIGHT:
                    direction += new Vector3(1, 0, 0);
                    break;
                case Direction.DOWN:
                    direction += new Vector3(0, 1, 0);
                    break;
                case Direction.UP_LEFT:
                    direction += new Vector3(-1, -1, 0);
                    break;
                case Direction.UP_RIGHT:
                    direction += new Vector3(1, -1, 0);
                    break;
                case Direction.DOWN_LEFT:
                    direction += new Vector3(-1, 1, 0);
                    break;
                case Direction.DOWN_RIGHT:
                    direction += new Vector3(1, 1, 0);
                    break;
            }
            Coordinates += GetNormalizedIncrement(direction, elapsedTime);
        }

        /// <summary>
        /// Clamps Coordinates to the destination point
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="destination"></param>
        /// <param name="increment"></param>
        /// <returns></returns>
        private void ClampCoordinatesToDestination(
            Vector3 destination,
            Vector3 increment)
        {
            var distance = Vector3.Abs(Coordinates - destination);
            var absoluteIncrement = Vector3.Abs(increment);

            Coordinates = distance.X < absoluteIncrement.X && distance.Y < absoluteIncrement.Y
                ? destination
                : Coordinates += increment;
        }

        /// <summary>
        /// Clamps the coordinates to the world
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="maxX"></param>
        /// <param name="maxY"></param>
        /// <returns></returns>
        private void ClampCoordinates(int maxX, int maxY)
        {
            var min = new Vector3(0, 0, 0);
            var max = new Vector3(maxX, maxY, 0);
            Coordinates = Vector3.Clamp(Coordinates, min, max);
        }

        /// <summary>
        /// Get coordinates in direction of point
        /// </summary>
        /// <param name="center"></param>
        /// <param name="point"></param>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        private Vector3 GetCoordinatesToPoint(
            Vector3 center,
            Vector3 point,
            double elapsedTime)
        {
            var direction = point - center;
            return GetNormalizedIncrement(direction, elapsedTime);
        }

        /// <summary>
        /// Get the normalized increment for a given direction
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="elapsedTime"></param>
        /// <returns></returns>
        private Vector3 GetNormalizedIncrement(Vector3 direction, double elapsedTime)
        {
            return direction == Vector3.Zero
                ? Vector3.Zero
                : Vector3.Normalize(direction) * (float)elapsedTime * MovementSpeed;
        }
    }
}
