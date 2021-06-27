﻿using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Parser
{
    public interface IPacketParser
    {
        public IPacket ReadPacket(PacketType packetId, PacketReader packetReader);

        public byte[] WritePacket(IPacket packet, PacketWriter packetWriter);
    }
}
