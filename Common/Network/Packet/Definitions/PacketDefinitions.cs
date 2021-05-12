using System;
using Common.Network.Packet.Exceptions;

namespace Common.Network.Packet.Definitions
{
    public static class PacketDefinitions
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
