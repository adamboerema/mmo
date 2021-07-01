using System;
using System.Collections.Generic;

namespace Common.Definitions
{
    public interface IPacketDefinitions
    {
        public Dictionary<PacketType, IPacket> Packets { get; }

    }
}
