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
        public Vector3 Coordinates => _movement.Coordinates;

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


        /// <summary>
        /// Game tick
        /// </summary>
        /// <param name="gameTick"></param>
        public void Update(GameTick gameTick, World world)
        {
            _movement.Move(gameTick, world.Width, world.Height);
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
