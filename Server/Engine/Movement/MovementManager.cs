using System;
using System.Numerics;
using Common.Base;
using Common.Extensions;
using Common.Packets.ServerToClient.Movement;
using Server.Bus.Packet;
using Server.Engine.Player;

namespace Server.Engine.Movement
{
    public class MovementManager: IMovementManager
    {
        private const int MAX_WIDTH = 1000;
        private const int MAX_HEIGHT = 1000;

        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IPlayerStore _playerStore;

        public MovementManager(
            IDispatchPacketBus dispatchPacketBus,
            IPlayerStore playerStore)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _playerStore = playerStore;
        }

        public void Update(double elapsedTime, double timestamp)
        {
            UpdateCoordinatesOfPlayers(elapsedTime);
        }

        public void UpdateMovementInput(string playerId, MovementType movementType)
        {
            var player = _playerStore.Get(playerId);
            if (player != null)
            {
                player.MovementType = movementType;
                _playerStore.Update(player);
                DispatchMovementUpdate(player);
            }
        }

        /// <summary>
        /// Updates the coordinates of the player
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void UpdateCoordinatesOfPlayers(double elapsedTime)
        {
            var players = _playerStore.GetAll();
            foreach (var player in players.Values)
            {
                var speed = player.MovementSpeed * (float) elapsedTime;
                player.MoveCoordinates(speed, MAX_WIDTH, MAX_HEIGHT);
                _playerStore.Update(player);
            }
        }

        /// <summary>
        /// Dispatch the movement update to all of the clients
        /// </summary>
        /// <param name="player"></param>
        private void DispatchMovementUpdate(PlayerModel player)
        {
            _dispatchPacketBus.Publish(new MovementOutputPacket
            {
                PlayerId = player.Id,
                Position = new Vector3(
                    player.Coordinates.X,
                    player.Coordinates.Y,
                    player.Coordinates.Z),
                MovementType = player.MovementType
            });
        }
    }
}
