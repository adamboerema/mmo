using System;
using System.Numerics;
using Common.Definitions;
using Common.Model;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Enemy
{
    public class EnemyMovementPacket: IPacket
    {
        public PacketType Id => PacketType.ENEMY_MOVEMENT;
        public string EnemyId { get; set; }
        public Vector3 Position { get; set; }
        public MovementType MovementType { get; set; }
        public float MovementSpeed { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            EnemyId = packetReader.ReadString();
            Position = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            MovementType = (MovementType)packetReader.ReadInteger();
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
            packetWriter.WriteInteger((int)MovementType);
            packetWriter.WriteFloat(MovementSpeed);
            return packetWriter.ToBytes();
        }
    }
}
