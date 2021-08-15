using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemyEngagePacket: IPacket
    {
        public PacketType Id => PacketType.ENEMY_ENGAGE;
        public string EnemyId { get; set; }
        public string TargetId { get; set; }
        public float MovementSpeed { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
            TargetId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(EnemyId);
            packetWriter.WriteString(TargetId);
            return packetWriter.ToBytes();
        }
    }
}
