using System;
using System.Numerics;
using Common.Model;
using Common.Packets.ClientToServer.Movement;
using CommonClient.Bus.Packet;
using CommonClient.Engine.Player;

namespace CommonClient.Engine.Movement
{
    public class MovementManager: IMovementManager
    {
        private readonly IDispatchPacketBus _dispatchPacket;
        private readonly IPlayerStore _playersStore;

        public MovementManager(IDispatchPacketBus dispatchPacketBus, IPlayerStore playersStore)
        {
            _dispatchPacket = dispatchPacketBus;
            _playersStore = playersStore;
        }

        public void UpdateMovementInput(MovementType movementType)
        {
            _dispatchPacket.Publish(new MovementInputPacket
            {
                Direction = movementType
            });
        }

        public void UpdatePlayerCoordinates(string playerId, Vector3 coordinates, MovementType movementType)
        {
            _playersStore.UpdateMovement(playerId, coordinates, movementType);
        }
    }
}
