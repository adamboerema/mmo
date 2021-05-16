﻿using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions.Server;

namespace Common.Network.Packet.Definitions
{
    public class ServerDefinitions : IPacketDefinitions
    {
        public static int LOGIN_RESPONSE = 0;

        public Dictionary<int, IPacket> Packets => new Dictionary<int, IPacket>
        {
            { LOGIN_RESPONSE, new LoginResponsePacket() }
        };
    }
}
