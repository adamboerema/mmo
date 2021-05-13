using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public interface IPacket
    {
        IPacket ParseData(PacketReader packetReader);
    }
}
