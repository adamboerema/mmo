using System;
using Common.Network.Definitions;
using Common.Network.IO;
using Common.Network.Parser;

namespace Common.Network.Manager
{
    public class PacketManager: IPacketManager
    {
        private IPacketParser packetParser;

        public PacketManager(IPacketDefinitions packetDefinitions)
        {
            packetParser = new PacketParser(packetDefinitions);
        }

        public IPacket Receive(byte[] bytes)
        {
            var reader = new PacketReader(bytes);
            var packetId = reader.ReadInteger();
            var packet = packetParser.ReadPacket((PacketType)packetId, reader);
            reader.Dispose();

            Console.WriteLine($"Packet Id: {packetId} -- Packet {packet}");

            return packet;
        }

        public byte[] Write(IPacket packet)
        {
            var writer = new PacketWriter();
            var output = packetParser.WritePacket(packet, writer);
            writer.Dispose();
            return output;
        }
    }
}
