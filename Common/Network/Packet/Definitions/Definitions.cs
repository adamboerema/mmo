using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Schema.Auth;

namespace Common.Network.Packet.Definitions
{
    public class Definitions: IPacketDefinitions
    {
        public const int LOGIN_REQUEST = 0;
        public const int LOGIN_RESPONSE = 1;

        public Dictionary<int, IPacketEvent> Packets => new Dictionary<int, IPacketEvent>
        {
            { LOGIN_REQUEST, new LoginRequestPacket() },
            { LOGIN_RESPONSE, new LoginResponsePacket() }
        };
    }
}
