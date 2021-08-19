using System;
using System.Numerics;
using Common.Definitions;
using Common.Base;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemySpawnPacket : IPacket
    {
        public PacketType Id => PacketType.ENEMY_SPAWN;
        public string EnemyId { get; set; }
        public EnemyType Type { get; set; }
        public string? TargetId { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 MovementDestination { get; set;}
        public float MovementSpeed { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
            Type = (EnemyType)packetReader.ReadInteger();
            TargetId = packetReader.ReadNullableString();
            Position = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            MovementDestination = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            MovementSpeed = packetReader.ReadFloat();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(EnemyId);
            packetWriter.WriteInteger((int)Type);
            packetWriter.WriteNullableString(TargetId);
            packetWriter.WriteFloat(Position.X);
            packetWriter.WriteFloat(Position.Y);
            packetWriter.WriteFloat(Position.Z);
            packetWriter.WriteFloat(MovementDestination.X);
            packetWriter.WriteFloat(MovementDestination.Y);
            packetWriter.WriteFloat(MovementDestination.Z);
            packetWriter.WriteFloat(MovementSpeed);
            return packetWriter.ToBytes();
        }
    }
}
