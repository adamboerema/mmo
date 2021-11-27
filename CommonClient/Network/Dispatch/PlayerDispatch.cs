using System;
using Common.Model.Shared;
using Common.Packets.ClientToServer.Player;
using CommonClient.Bus.Packet;

namespace CommonClient.Network.Dispatch
{
    public class PlayerDispatch: IPlayerDispatch
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;

        public PlayerDispatch(IDispatchPacketBus dispatchPacketBus)
        {
            _dispatchPacketBus = dispatchPacketBus;
        }

        public void DispatchPlayerMovement(
            Direction direction,
            bool isMoving)
        {
            var packet = new PlayerMovementPacket
            {
                Direction = direction,
                IsMoving = isMoving
            };
            _dispatchPacketBus.Publish(packet);
        }
    }
}
