using System;
using Common.Definitions;
using Common.Network.Exceptions;
using Common.Network.IO;

namespace Common.Network.Parser
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

        public IPacket ReadPacket(byte[] bytes)
        {
            using(var reader = new PacketReader(bytes))
            {
                var packetId = reader.ReadInteger();
                return ReadPacket((PacketType)packetId, reader);
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
