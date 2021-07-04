using System;
using Common.Definitions;
using Common.Model;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Movement
{
    public class MovementOutputPacket: IPacket
    {
        public PacketType Id => PacketType.MOVEMENT_OUTPUT;

        public string PlayerId { get; set; }

        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public MovementType MovementType { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            X = packetReader.ReadFloat();
            Y = packetReader.ReadFloat();
            Z = packetReader.ReadFloat();
            MovementType = (MovementType)packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(PlayerId);
            packetWriter.WriteFloat(X);
            packetWriter.WriteFloat(Y);
            packetWriter.WriteFloat(Z);
            packetWriter.WriteInteger((int)MovementType);
            return packetWriter.ToBytes();
        }
    }
}
