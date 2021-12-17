using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemyAttackPacket: IPacket
    {
        public PacketType Id => PacketType.ENEMY_ATTACK;
        public string EnemyId { get; set; }
        public string TargetId { get; set; }
        public int Damage { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
            TargetId = packetReader.ReadString();
            Damage = packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(EnemyId);
            packetWriter.WriteString(TargetId);
            packetWriter.WriteInteger(Damage);
            return packetWriter.ToBytes();
        }
    }
}
