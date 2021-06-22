using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public class ReceiverPacketEvent
    {
        /// <summary>
        /// Id of the connection to a client
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Packet to be sent or received
        /// </summary>
        public IPacket Packet { get; set; }
    }
}
