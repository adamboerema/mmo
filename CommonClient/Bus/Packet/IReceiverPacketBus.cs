using System;
using Common.Bus;
using Common.Definitions;

namespace CommonClient.Bus.Packet
{
    public interface IReceiverPacketBus : IEventBus<PacketEvent>
    {
        /// <summary>
        /// Receive a packet with connection id information
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Publish(IPacket packetEvent);
    }
}

