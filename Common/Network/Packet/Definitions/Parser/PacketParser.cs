using System;
using Common.Network.Packet.Exceptions;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Parser
{
    public class PacketParser : IPacketParser
    {
        private IPacketDefinitions _definitions;

        public PacketParser(IPacketDefinitions packetDefinitions)
        {
            _definitions = packetDefinitions;
        }

        public IPacketEvent ReadPacket(int packetId, PacketReader packetReader)
        {
            try
            {
                var packet = _definitions.Packets[packetId];
                packet.ReadData(packetReader);
                return packet;
            }
            catch (Exception exception)
            {
                throw new PacketDefinitionException(packetId, exception);
            }
        }

        public byte[] WritePacket(IPacketEvent packet, PacketWriter packetWriter)
        {
            try
            {
                return packet.WriteData(packetWriter);
            }
            catch (Exception exception)
            {
                throw new PacketDefinitionException(packet.Id, exception);
            }
        }
    }
}
