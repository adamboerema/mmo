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
        private readonly IEnemyStore _enemyStore;

        public EnemyDispatch(
            IDispatchPacketBus dispatchPacketBus,
            IEnemyStore enemyStore)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _enemyStore = enemyStore;
        }

        public void DispatchEnemyDisenage(string id)
        {
            var packet = new EnemyDisengagePacket
            {
                EnemyId = id
            };
            _dispatchPacketBus.Publish(packet);
        }

        public void DispatchEnemyEngage(string id, string targetId)
        {
            var packet = new EnemyEngagePacket
            {
                EnemyId = id,
                TargetId = targetId
            };
            _dispatchPacketBus.Publish(packet);
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
            _dispatchPacketBus.Publish(packet);
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
            _dispatchPacketBus.Publish(packet);
        }
    }
}
