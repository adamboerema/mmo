using System;
using Common.Packets.ServerToClient.Enemy;
using CommonClient.Engine.Enemy;

namespace CommonClient.Network.Handler
{
    public class EnemyHandler:
        IPacketHandler<EnemySpawnPacket>,
        IPacketHandler<EnemyMovementPacket>,
        IPacketHandler<EnemyEngagePacket>,
        IPacketHandler<EnemyDisengagePacket>
    {
        private readonly IEnemyManager _enemyManager;

        public EnemyHandler(IEnemyManager enemyManager)
        {
            _enemyManager = enemyManager;
        }

        public void Handle(EnemySpawnPacket packet)
        {
            _enemyManager.SpawnEnemy(
                packet.EnemyId,
                packet.Type,
                packet.TargetId,
                packet.Position,
                packet.MovementDestination,
                packet.MovementSpeed);
        }

        public void Handle(EnemyMovementPacket packet)
        {
            _enemyManager.MoveEnemy(
                packet.EnemyId,
                packet.Position,
                packet.MovementDestination,
                packet.MovementSpeed);
        }

        public void Handle(EnemyEngagePacket packet)
        {
            _enemyManager.EngageEnemy(
                packet.EnemyId,
                packet.TargetId);
        }

        public void Handle(EnemyDisengagePacket packet)
        {
            _enemyManager.DisengageEnemy(packet.EnemyId);
        }
    }
}
