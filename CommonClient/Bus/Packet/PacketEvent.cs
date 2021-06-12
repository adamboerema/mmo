using System;
using Common.Network.Packet.Definitions;

namespace CommonClient.Bus.Packet
{
    public class PacketEvent
    {
        public IPacket Packet { get; set; }
    }
}
