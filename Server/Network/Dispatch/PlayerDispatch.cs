using System;
using System.Numerics;
using Common.Model.Shared;
using Common.Packets.ServerToClient.Player;
using Common.Packets.ServerToClient.Player;
using Common.Store;
using Server.Bus.Packet;
using Server.Component.Player;
using Server.Engine.Player;

namespace Server.Network.Dispatch
{
    public class PlayerDispatch: IPlayerDispatch
    {
        IDispatchPacketBus _dispatchPacketBus;
        ComponentStore<PlayerComponent> _playerStore;

        public PlayerDispatch(
            IDispatchPacketBus dispatchPacketBus,
            ComponentStore<PlayerComponent> playerStore)
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
            var packet = new PlayerMovementOutputPacket
            {
                PlayerId = playerId,
                Position = coordinates,
                MovementType = direction,
                IsMoving = isMoving
            };
            _dispatchPacketBus.PublishAll(packet);
        }

        public void DispatchClientConnect(
            string connectionId,
            bool isMoving,
            Vector3 position,
            Direction direction)
        {
            var clientPacket = new PlayerConnectPacket
            {
                PlayerId = connectionId,
                IsClient = true,
                IsMoving = isMoving,
                Position = position,
                MovementType = direction
            };

            var allPlayerPacket = new PlayerConnectPacket
            {
                PlayerId = connectionId,
                IsClient = false,
                IsMoving = isMoving,
                Position = position,
                MovementType = direction
            };

            _dispatchPacketBus.Publish(connectionId, clientPacket);
            _dispatchPacketBus.PublishExcept(connectionId, allPlayerPacket);
        }

        public void DispatchPlayerConnect(
            string connectionId,
            string playerId,
            bool isMoving,
            Vector3 position,
            Direction direction)
        {
            var packet = new PlayerConnectPacket
            {
                PlayerId = playerId,
                IsClient = false,
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
            _dispatchPacketBus.PublishAll(packet);
        }

        public void DispatchPlayerAttack(
            string playerId,
            string targetId,
            int damage)
        {
            var packet = new PlayerAttackOutputPacket
            {
                PlayerId = playerId,
                TargetId = targetId,
                Damage = damage
            };
            _dispatchPacketBus.PublishAll(packet);
        }
    };
}
