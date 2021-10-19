using System;
using System.Numerics;
using Common.Model.Character;

namespace Common.Base
{
    public class PlayerModel
    {
        public string Id { get; init; }

        private CharacterModel _character { get; set; }

        private MovementModel _movement { get; init; }

        public PlayerModel(
            string id,
            CharacterModel characterModel,
            MovementModel movementModel)
        {
            Id = id;
            _character = characterModel;
            _movement = movementModel;
        }


        public Vector3 Coordinates => _movement.Coordinates;
        public Direction Direction => _movement.Direction;
        public float MovementSpeed => _movement.MovementSpeed;
        public bool IsMoving => _movement.IsMoving;


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
            _movement.Coordinates = coordinates;
            _movement.Direction = direction;
            _movement.IsMoving = isMoving;
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
            _movement.Direction = direction;
            _movement.IsMoving = isMoving;
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
            _movement.Move(elapsedTime, maxWidth, maxHeight);
            return this;
        }
    }
}
