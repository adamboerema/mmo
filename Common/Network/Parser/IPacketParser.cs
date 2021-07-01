using System;
using Common.Definitions;
using Common.Network.IO;

namespace Common.Network.Parser
{
    public interface IPacketParser
    {
        public IPacket ReadPacket(PacketType packetId, PacketReader packetReader);

        public byte[] WritePacket(IPacket packet, PacketWriter packetWriter);
    }
}
