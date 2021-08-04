using System;
using System.Numerics;
using Common.Definitions;
using Common.Model;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemySpawnPacket: IPacket
    {
        public PacketType Id => PacketType.ENEMY_SPAWN;
        public string EnemyId { get; set; }
        public EnemyType Type { get; set; }
        public Vector3 Position { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
            Type = (EnemyType)packetReader.ReadInteger();
            Position = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(EnemyId);
            packetWriter.WriteInteger((int)Type);
            packetWriter.WriteFloat(Position.X);
            packetWriter.WriteFloat(Position.Y);
            packetWriter.WriteFloat(Position.Z);
            return packetWriter.ToBytes();
        }
    }
}
