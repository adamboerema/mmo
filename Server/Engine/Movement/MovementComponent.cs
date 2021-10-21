using System;
using System.Numerics;
using Common.Entity;
using Common.Model.Shared;
using Common.Packets.ServerToClient.Movement;
using Common.Utility;
using Server.Bus.Packet;
using Server.Engine.Player;

namespace Server.Engine.Movement
{
    public class MovementComponent: IMovementComponent
    {
        private const int MAX_WIDTH = 1000;
        private const int MAX_HEIGHT = 1000;

        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IPlayerStore _playerStore;

        public MovementComponent(
            IDispatchPacketBus dispatchPacketBus,
            IPlayerStore playerStore)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _playerStore = playerStore;
        }

        public void Update(GameTick gameTick)
        {
            UpdateCoordinatesOfPlayers(gameTick);
        }

        public void UpdateMovementInput(
            string playerId,
            Direction movementType,
            bool isMoving)
        {
            var player = _playerStore.Get(playerId);
            if (player != null)
            {
                player.UpdateDirection(movementType, isMoving);
                _playerStore.Update(player);
                DispatchMovementUpdate(player);
            }
        }

        /// <summary>
        /// Updates the coordinates of the player
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void UpdateCoordinatesOfPlayers(GameTick gameTick)
        {
            var players = _playerStore.GetAll();
            foreach (var player in players.Values)
            {
                player.Update(gameTick, WorldUtility.GetWorld());
                _playerStore.Update(player);
            }
        }

        /// <summary>
        /// Dispatch the movement update to all of the clients
        /// </summary>
        /// <param name="player"></param>
        private void DispatchMovementUpdate(PlayerEntity player)
        {
            _dispatchPacketBus.Publish(new MovementOutputPacket
            {
                PlayerId = player.Id,
                Position = new Vector3(
                    player.Coordinates.X,
                    player.Coordinates.Y,
                    player.Coordinates.Z),
                MovementType = player.Direction,
                IsMoving = player.IsMoving
            });
        }
    }
}
