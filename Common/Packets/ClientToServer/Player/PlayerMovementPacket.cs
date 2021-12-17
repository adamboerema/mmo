using System;
using Common.Definitions;
using Common.Network.IO;
using Common.Model.Shared;

namespace Common.Packets.ClientToServer.Player
{
    public class PlayerMovementPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_MOVEMENT_INPUT;

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
