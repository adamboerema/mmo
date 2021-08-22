using System;
using Common.Definitions;
using Common.Base;
using Common.Network.IO;

namespace Common.Packets.ClientToServer.Movement
{
    public class MovementInputPacket: IPacket
    {
        public PacketType Id => PacketType.MOVEMENT_INPUT;

        public Direction Direction { get; set; }

        public bool IsMoving { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            Direction = (Direction)packetReader.ReadInteger();
            IsMoving = packetReader.ReadBool();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteInteger((int)Direction);
            packetWriter.WriteBool(IsMoving);
            return packetWriter.ToBytes();
        }
    }
}
