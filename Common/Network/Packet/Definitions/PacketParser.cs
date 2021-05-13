using System;
using Common.Network.Packet.Exceptions;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public class PacketParser : IPacketParser
    {
        public IPacket ParsePacket(int packetId, PacketReader packetReader)
        {
            try
            {
                var packet = ServerDefinitions.Packets[packetId];
                packet.ParseData(packetReader);
                return packet;
            }
            catch (Exception exception)
            {
                throw new PacketDefinitionException(packetId, exception);
            }
        }
    }
}
