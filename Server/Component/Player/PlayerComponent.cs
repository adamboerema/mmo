using System;
using System.Numerics;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;
using Server.Network.Dispatch;

namespace Server.Component.Player
{
    public class PlayerComponent: IComponent
    {
        public readonly string Id;

        private readonly CharacterModel _character;
        private readonly MovementModel _movement;
        private readonly IPlayerDispatch _playerDispatch;

        public PlayerComponent(
            PlayerConfiguration configuration,
            IPlayerDispatch playerDispatch)
        {
            Id = configuration.Id;
            _character = configuration.Character;
            _movement = configuration.Movement;
            _playerDispatch = playerDispatch;
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
        public void UpdateCoordinates(
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            _movement.Coordinates = coordinates;
            _movement.Direction = direction;
            _movement.IsMoving = isMoving;
        }

        /// <summary>
        /// Update Direction of the player
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="isMoving"></param>
        /// <returns></returns>
        public void UpdateDirection(Direction direction, bool isMoving)
        {
            _movement.Direction = direction;
            _movement.IsMoving = isMoving;

            _playerDispatch.DispatchMovementUpdate(
                Id,
                _movement.Coordinates,
                _movement.Direction,
                _movement.IsMoving);
        }
    }
}
