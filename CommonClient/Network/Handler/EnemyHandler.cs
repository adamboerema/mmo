using System;
using Common.Packets.ServerToClient.Enemy;
using CommonClient.Engine.Enemy;

namespace CommonClient.Network.Handler
{
    public class EnemyHandler:
        IPacketHandler<EnemySpawnPacket>,
        IPacketHandler<EnemyMovementPacket>
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
                packet.Position);
        }

        public void Handle(EnemyMovementPacket packet)
        {
            _enemyManager.MoveEnemy(
                packet.EnemyId,
                packet.Position,
                packet.MovementType,
                packet.MovementSpeed);
        }
    }
}
