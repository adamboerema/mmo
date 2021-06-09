using System;

namespace Server.Bus.Packet
{
    public interface IDispatchPacketBus: IEventBus<PacketEvent>
    {
        /// <summary>
        /// Dispatch a packet with connection id information
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="packet"></param>
        public void Publish(string connectionId, PacketEvent packet);
    }
}
