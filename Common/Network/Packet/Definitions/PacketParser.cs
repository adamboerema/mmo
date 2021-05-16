using System;
using Common.Network.Packet.Exceptions;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions
{
    public class PacketParser : IPacketParser
    {
        private IPacketDefinitions definitions;

        public PacketParser(IPacketDefinitions packetDefinitions)
        {
            definitions = packetDefinitions;
        }

        public IPacket ReadPacket(int packetId, PacketReader packetReader)
        {
            try
            {
                var packet = definitions.Packets[packetId];
                packet.ReadData(packetReader);
                return packet;
            }
            catch (Exception exception)
            {
                throw new PacketDefinitionException(packetId, exception);
            }
        }

        public byte[] WritePacket(IPacket packet, PacketWriter packetWriter)
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
