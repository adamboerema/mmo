using System;
using Common.Model;
using Common.Packets.ClientToServer.Movement;
using CommonClient.Bus.Packet;
using CommonClient.Engine.Player;

namespace CommonClient.Engine.Movement
{
    public class MovementManager: IMovementManager
    {
        private readonly IDispatchPacketBus _dispatchPacket;
        private readonly IPlayersStore _playerStore;

        public MovementManager(IDispatchPacketBus dispatchPacketBus)
        {
            _dispatchPacket = dispatchPacketBus;
        }

        public void DispatchMovementInput(MovementType movementType)
        {
            _dispatchPacket.Publish(new MovementInputPacket
            {
                Direction = movementType
            });
        }

        public void ReceiveUpdateMovement(string playerId, int x, int y, int z, MovementType movementType)
        {
            _playerStore.UpdateMovement(playerId, x, y, z, movementType);
        }
    }
}
