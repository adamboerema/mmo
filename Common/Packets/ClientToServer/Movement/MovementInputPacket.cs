using System;
using Common.Definitions;
using Common.Model;
using Common.Network.IO;

namespace Common.Packets.ClientToServer.Movement
{
    public class MovementInputPacket: IPacket
    {
        public PacketType Id => PacketType.MOVEMENT_INPUT;

        public MovementType Direction { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            Direction = (MovementType)packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteInteger((int)Direction);
            return packetWriter.ToBytes();
        }
    }
}
