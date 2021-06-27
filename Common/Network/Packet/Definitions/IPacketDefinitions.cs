using System;
using System.Collections.Generic;

namespace Common.Network.Packet.Definitions
{
    public interface IPacketDefinitions
    {
        public Dictionary<PacketType, IPacket> Packets { get; }
    }
}
