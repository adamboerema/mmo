using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public interface IPacket
    {
        public int Id { get; }

        public IPacket ReadData(PacketReader packetReader);

        public byte[] WriteData(PacketWriter packetWriter);
    }
}
