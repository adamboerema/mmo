using System;
using Common.Definitions;

namespace Server.Bus.Packet
{
    public enum DispatchType
    {
        ALL,
        ALL_EXCEPT,
        CONNECTION
    }

    public class DispatchPacketEvent
    {
        /// <summary>
        /// Type of dispatch
        /// </summary>
        public DispatchType Type { get; set; }

        /// <summary>
        /// Id of the connection to a client
        /// 
        /// </summary>
        public string ConnectionId { get; set; }

        /// <summary>
        /// Packet to be sent or received
        /// </summary>
        public IPacket Packet { get; set; }
    }
}
