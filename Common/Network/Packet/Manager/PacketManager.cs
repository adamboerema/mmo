using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Manager
{
    public class PacketManager: IPacketManager
    {
        private PacketReader reader;
        private PacketWriter writer;

        private IList<IPacketDefinition> packetDefinitions;

        public PacketManager()
        {
        }

        public void HandleReceivePacket(byte[] bytes)
        {
            reader = new PacketReader(bytes);
            var packetId = reader.ReadInteger();
            reader.Dispose();

            var packet = ConvertManager.GetDefinition(packetId);

        }
    }
}
