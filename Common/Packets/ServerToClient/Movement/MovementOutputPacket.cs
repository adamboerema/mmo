using System;
using System.Numerics;
using Common.Definitions;
using Common.Model;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Movement
{
    public class MovementOutputPacket: IPacket
    {
        public PacketType Id => PacketType.MOVEMENT_OUTPUT;

        public string PlayerId { get; set; }
        public Vector3 Position { get; set; }
        public MovementType MovementType { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            Position = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            MovementType = (MovementType)packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(PlayerId);
            packetWriter.WriteFloat(Position.X);
            packetWriter.WriteFloat(Position.Y);
            packetWriter.WriteFloat(Position.Z);
            packetWriter.WriteInteger((int)MovementType);
            return packetWriter.ToBytes();
        }
    }
}
