using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Server;

namespace Common.Network.Packet.Definitions
{
    public static class ServerDefinitions
    {
        public static Dictionary<int, IPacket> Packets = new Dictionary<int, IPacket>
        {
            { 0, new LoginPacket() }
        };
    }
}
