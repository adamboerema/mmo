using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public class PacketEvent
    {
        /// <summary>
        /// Id of the connection to a client
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Packet to be sent or received
        /// </summary>
        public IPacketEvent Packet { get; set; }
    }
}
