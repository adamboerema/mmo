using System;
using System.Numerics;
using Common.Component;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;
using CommonClient.Network.Dispatch;

namespace CommonClient.ComponentStore.Player
{
    public class PlayerComponent: IComponent
    {
        public string Id { get; init; }
        public bool IsClient { get; init; }
        public Vector3 Coordinates => _movement.Coordinates;

        private CharacterModel _charater;
        private MovementModel _movement;

        private IPlayerDispatch _playerDispatch;

        public PlayerComponent(
            PlayerConfiguration playerConfiguration,
            IPlayerDispatch playerDispatch)
        {
            Id = playerConfiguration.Id;
            IsClient = playerConfiguration.IsClient;
            _charater = playerConfiguration.Character;
            _movement = playerConfiguration.Movement;

            _playerDispatch = playerDispatch;
        }

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
            _playerDispatch.DispatchPlayerMovement(direction, isMoving);
        }

        //public void UpdateMovement(
        //    string playerId,
        //    Vector3 coordinates,
        //    Direction direction,
        //    bool isMoving)
        //{
        //    var player = Get(playerId);
        //    if(player != null)
        //    {
        //        player.UpdateCoordinates(coordinates, direction, isMoving);
        //        Update(player);
        //    }
        //}

        //public void UpdateClientCoordinates(
        //    Vector3 coordinates,
        //    Direction movementType,
        //    bool isMoving)
        //{
        //    UpdateMovement(_clientPlayerId, coordinates, movementType, isMoving);
        //}

        //public void UpdateClientMovement(Direction direction, bool isMoving)
        //{
        //    var clientPlayer = GetClientPlayer();
        //    if(clientPlayer != null)
        //    {
        //        clientPlayer.UpdateDirection(direction, isMoving);
        //        Update(clientPlayer);
        //    }
        //}

    }
}
