using System;
using System.Collections.Generic;

namespace Common.Network.Definitions
{
    public interface IPacketDefinitions
    {
        public Dictionary<PacketType, IPacket> Packets { get; }

    }
}
