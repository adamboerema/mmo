using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Exceptions;

namespace Common.Network.Packet.Manager
{
    public static class ConvertManager
    {
        public static IPacketDefinition GetDefinition(int packetId)
        {
            try
            {
                return PacketList[packetId];
            } catch(Exception exception)
            {
                throw new PacketDefinitionException(packetId, exception);
            }
        }

        private static IPacketDefinition[] PacketList = new IPacketDefinition[]
        {
        };
    }
}
