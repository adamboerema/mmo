using System;
using Common.Bus;
using Common.Network.Definitions;

namespace Server.Bus.Packet
{
    public interface IReceiverPacketBus: IEventBus<ReceiverPacketEvent>
    {
        /// <summary>
        /// Receive a packet with connection id information
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Publish(string connectionId, IPacket packetEvent);
    }
}
