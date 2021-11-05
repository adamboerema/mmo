using System;
using System.Numerics;
using Common.Model.Shared;
using Common.Packets.ServerToClient.Movement;
using Common.Packets.ServerToClient.Player;
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
            var packet = new MovementOutputPacket
            {
                PlayerId = playerId,
                Position = coordinates,
                MovementType = direction,
                IsMoving = isMoving
            };
            _dispatchPacketBus.Publish(packet);
        }

        public void DispatchPlayerConnect(
            string connectionId,
            string playerId,
            bool isClient,
            bool isMoving,
            Vector3 position,
            Direction direction)
        {
            var packet = new PlayerConnectPacket
            {
                PlayerId = playerId,
                IsClient = isClient,
                IsMoving = isMoving,
                Position = position,
                MovementType = direction
            };
            _dispatchPacketBus.Publish(connectionId, packet);
         }

        public void DispatchPlayerDisconnect(string playerId)
        {
            var packet = new PlayerDisconnectPacket
            {
                PlayerId = playerId
            };
            _dispatchPacketBus.Publish(packet);
        }

    };
}
