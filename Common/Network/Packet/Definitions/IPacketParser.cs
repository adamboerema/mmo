using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public interface IPacketParser
    {
        public IPacket ReadPacket(int packetId, PacketReader packetReader);

        public byte[] WritePacket(IPacket packet, PacketWriter packetWriter);
    }
}
