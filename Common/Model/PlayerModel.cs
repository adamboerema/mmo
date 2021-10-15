using System;
using System.Numerics;
using Common.Model.Character;

namespace Common.Base
{
    public class PlayerModel
    {
        public string Id { get; init; }

        private CharacterModel Character { get; init; }

        public PlayerModel(
            string id,
            CharacterModel character)
        {
            Id = id;
            Character = character;
        }


        public Vector3 Coordinates => Character.Coordinates;
        public Direction Direction => Character.Direction;
        public float MovementSpeed => Character.MovementSpeed;
        public bool IsMoving => Character.IsMoving;


        /// <summary>
        /// Directly update the coordinates of player
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="direction"></param>
        /// <param name="isMoving"></param>
        /// <returns></returns>
        public PlayerModel UpdateCoordinates(
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            Character.Coordinates = coordinates;
            Character.Direction = direction;
            Character.IsMoving = isMoving;
            return this;
        }

        /// <summary>
        /// Update Direction of the player
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="isMoving"></param>
        /// <returns></returns>
        public PlayerModel UpdateDirection(Direction direction, bool isMoving)
        {
            Character.Direction = direction;
            Character.IsMoving = isMoving;
            return this;
        }

        /// <summary>
        /// Move Coordinates of player
        /// </summary>
        /// <param name="model">Player model</param>
        /// <param name="elapsedTime">Increment of time</param>
        /// <param name="maxWidth">Max world width</param>
        /// <param name="maxHeight">Max world height</param>
        /// <returns></returns>
        public PlayerModel Move(
            double elapsedTime,
            int maxWidth,
            int maxHeight)
        {
            Character.Move(elapsedTime, maxWidth, maxHeight);
            return this;
        }
    }
}
