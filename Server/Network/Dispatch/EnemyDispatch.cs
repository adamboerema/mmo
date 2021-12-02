using System;
using System.Numerics;
using Common.Model.Shared;
using Common.Packets.ServerToClient.Enemy;
using Server.Bus.Packet;
using Server.Engine.Enemy;

namespace Server.Network.Dispatch
{
    public class EnemyDispatch: IEnemyDispatch
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;

        public EnemyDispatch(
            IDispatchPacketBus dispatchPacketBus)
        {
            _dispatchPacketBus = dispatchPacketBus;
        }

        public void DispatchEnemyToPlayer(
            string playerId,
            string enemyId,
            EnemyType enemyType,
            Vector3 coordinates,
            float movementSpeed,
            string engageTargetId,
            Vector3 movementDestination)
        {
            var packet = new EnemySpawnPacket
            {
                EnemyId = enemyId,
                Type = enemyType,
                TargetId = engageTargetId,
                Position = coordinates,
                MovementDestination = movementDestination,
                MovementSpeed = movementSpeed,
            };
            _dispatchPacketBus.Publish(playerId, packet);
        }

        public void DispatchEnemyDisenage(string id)
        {
            var packet = new EnemyDisengagePacket
            {
                EnemyId = id
            };
            _dispatchPacketBus.PublishAll(packet);
        }

        public void DispatchEnemyEngage(string id, string targetId)
        {
            var packet = new EnemyEngagePacket
            {
                EnemyId = id,
                TargetId = targetId
            };
            _dispatchPacketBus.PublishAll(packet);
        }

        public void DispatchEnemyMovement(
            string id,
            Vector3 coordinates,
            Vector3 movementDestination,
            float movementSpeed)
        {
            var packet = new EnemyMovementPacket
            {
                EnemyId = id,
                Position = coordinates,
                MovementDestination = movementDestination,
                MovementSpeed = movementSpeed
            };
            _dispatchPacketBus.PublishAll(packet);
        }

        public void DispatchEnemySpawn(
            string id,
            EnemyType enemyType,
            Vector3 coordinates)
        {
            var packet = new EnemySpawnPacket
            {
                EnemyId = id,
                Type = enemyType,
                Position = coordinates,
            };
            _dispatchPacketBus.PublishAll(packet);
        }

        public void DispatchEnemyAttack(
            string id,
            string targetId,
            int damage)
        {
            var packet = new EnemyAttackPacket
            {
                EnemyId = id,
                TargetId = targetId,
                Damage = damage,
            };
            _dispatchPacketBus.PublishAll(packet);
        }
    }
}
