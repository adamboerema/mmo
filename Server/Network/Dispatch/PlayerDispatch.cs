using System;
using System.Numerics;
using Common.Model.Shared;
using Common.Packets.ServerToClient.Movement;
using Server.Bus.Packet;
using Server.Engine.Player;

namespace Server.Network.Dispatch
{
    public class PlayerDispatch: IPlayerDispatch
    {
        IDispatchPacketBus _dispatchPacketBus;
        IPlayerStore _playerStore;

        public PlayerDispatch(
            IDispatchPacketBus dispatchPacketBus,
            IPlayerStore playerStore)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _playerStore = playerStore;
        }

        public void DispatchMovementUpdate(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            _dispatchPacketBus.Publish(new MovementOutputPacket
            {
                PlayerId = playerId,
                Position = coordinates,
                MovementType = direction,
                IsMoving = isMoving
            });
        }
    }
}
