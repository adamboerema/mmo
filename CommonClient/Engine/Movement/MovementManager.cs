using System;
using System.Numerics;
using Common.Base;
using Common.Model.Shared;
using Common.Packets.ClientToServer.Movement;
using CommonClient.Bus.Packet;
using CommonClient.Engine.Player;

namespace CommonClient.Engine.Movement
{
    public class MovementManager: IMovementManager
    {
        private readonly IDispatchPacketBus _dispatchPacket;
        private readonly IPlayerStore _playerStore;

        public MovementManager(IDispatchPacketBus dispatchPacketBus, IPlayerStore playersStore)
        {
            _dispatchPacket = dispatchPacketBus;
            _playerStore = playersStore;
        }

        public void UpdateClientMovementInput(Direction direction, bool isMoving)
        {
            _playerStore.UpdateClientMovement(direction, isMoving);

            _dispatchPacket.Publish(new MovementInputPacket
            {
                Direction = direction,
                IsMoving = isMoving
            });
        }

        public void UpdatePlayerCoordinatesOutput(
            string playerId,
            Vector3 coordinates,
            Direction movementType,
            bool isMoving)
        {
            _playerStore.UpdateMovement(playerId, coordinates, movementType, isMoving);
        }
    }
}
