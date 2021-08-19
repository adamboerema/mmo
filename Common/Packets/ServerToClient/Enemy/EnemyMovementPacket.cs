using System;
using System.Numerics;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemyMovementPacket: IPacket
    {
        public PacketType Id => PacketType.ENEMY_MOVEMENT;
        public string EnemyId { get; set; }
        public Vector3 Position { get; set; }
        public Vector3 MovementDestination { get; set; }
        public float MovementSpeed { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
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
