using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public class PacketEvent
    {
        public string ConnectionId { get; set; }

        public IPacket Packet { get; set; }
    }
}
