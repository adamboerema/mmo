using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Parser
{
    public interface IPacketParser
    {
        public IPacketEvent ReadPacket(int packetId, PacketReader packetReader);

        public byte[] WritePacket(IPacketEvent packet, PacketWriter packetWriter);
    }
}
