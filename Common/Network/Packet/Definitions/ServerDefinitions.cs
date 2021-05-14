using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Server;

namespace Common.Network.Packet.Definitions
{
    public enum ServerPacketType : int
    {
        LOGIN = 0
    }

    public static class ServerDefinitions
    {
        public static Dictionary<ServerPacketType, IPacket> Packets = new Dictionary<ServerPacketType, IPacket>
        {
            { ServerPacketType.LOGIN, new LoginPacket() }
        };
    }
}
