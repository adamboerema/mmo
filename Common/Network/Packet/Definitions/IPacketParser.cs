using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public interface IPacketParser
    {
        public IPacket ParsePacket(int packetId, PacketReader packetReader);
    }
}
