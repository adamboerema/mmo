using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemyDisengagePacket : IPacket
    {
        public PacketType Id => PacketType.ENEMY_DISENGAGE;
        public string EnemyId { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(EnemyId);
            return packetWriter.ToBytes();
        }
    }
}

