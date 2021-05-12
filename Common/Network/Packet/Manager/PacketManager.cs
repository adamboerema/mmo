using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Manager
{
    public class PacketManager: IPacketManager
    {
        public PacketManager()
        {
        }

        public IPacketDefinition Receive(byte[] bytes)
        {
            var reader = new PacketReader(bytes);
            var packetId = reader.ReadInteger();
            reader.Dispose();

            return PacketDefinitions.GetDefinition(packetId);
        }

        public byte[] Write(byte[] bytes)
        {
            var writer = new PacketWriter(bytes.Length);
            writer.WriteBytes(bytes);
            return writer.ToArray();
        }
    }
}
