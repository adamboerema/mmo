using System;
using System.Collections.Generic;

namespace Common.Network.Packet.Definitions
{
    public interface IPacketDefinitions
    {
        public Dictionary<int, IPacketEvent> Packets { get; }
    }
}
