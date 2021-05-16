using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Client;

namespace Common.Network.Packet.Definitions
{
    public class ClientDefinitions: IPacketDefinitions
    {
        public static int LOGIN_REQUEST = 0;

        public Dictionary<int, IPacket> Packets => new Dictionary<int, IPacket>
        {
            { LOGIN_REQUEST, new LoginRequestPacket() }
        };
    }
}
