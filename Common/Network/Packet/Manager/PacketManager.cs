using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Manager
{
    public class PacketManager: IPacketManager
    {
        private IPacketParser packetParser;

        public PacketManager()
        {
            packetParser = new PacketParser();
        }

        public IPacket Receive(byte[] bytes)
        {
            var reader = new PacketReader(bytes);
            var packetId = reader.ReadInteger();
            var packet = packetParser.ParsePacket(packetId, reader);
            reader.Dispose();

            return packet;
        }

        public byte[] Write(byte[] bytes)
        {
            var writer = new PacketWriter(bytes.Length);
            writer.WriteBytes(bytes);
            var output = writer.ToArray();
            writer.Dispose();
            return output;
        }
    }
}
