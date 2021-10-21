using System;
using System.Numerics;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;

namespace Common.Entity
{
    public class PlayerEntity: IEntity
    {
        public string Id { get; init; }

        private CharacterModel _character { get; set; }

        private MovementModel _movement { get; init; }

        public PlayerEntity(
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
        /// Game tick
        /// </summary>
        /// <param name="gameTick"></param>
        public void Update(GameTick gameTick, World world)
        {
            _movement.Move(gameTick, world.Width, world.Height);
        }

        /// <summary>
        /// Directly update the coordinates of player
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="direction"></param>
        /// <param name="isMoving"></param>
        /// <returns></returns>
        public PlayerEntity UpdateCoordinates(
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
        public PlayerEntity UpdateDirection(Direction direction, bool isMoving)
        {
            _movement.Direction = direction;
            _movement.IsMoving = isMoving;
            return this;
        }
    }
}
