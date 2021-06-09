using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public interface IPacketEvent
    {
        public int Id { get; }

        public IPacketEvent ReadData(PacketReader packetReader);

        public byte[] WriteData(PacketWriter packetWriter);
    }
}
