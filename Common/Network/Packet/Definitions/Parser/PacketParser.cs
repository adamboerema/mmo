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

        public IPacket ReadPacket(PacketType packetId, PacketReader packetReader)
        {
            try
            {
                var packet = _definitions.Packets[packetId];
                packet.ReadData(packetReader);
                return packet;
            }
            catch (Exception exception)
            {
                throw new PacketDefinitionException((int)packetId, exception);
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
                throw new PacketDefinitionException((int)packet.Id, exception);
            }
        }
    }
}
