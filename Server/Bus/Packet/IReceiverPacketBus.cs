using System;
using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public interface IReceiverPacketBus: IEventBus<PacketEvent>
    {
        /// <summary>
        /// Receive a packet with connection id information
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Publish(string connectionId, PacketEvent packetEvent);
    }
}
