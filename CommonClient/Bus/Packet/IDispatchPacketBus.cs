using System;
using Common.Bus;
using Common.Definitions;

namespace CommonClient.Bus.Packet
{
    public interface IDispatchPacketBus : IEventBus<PacketEvent>
    {
        /// <summary>
        /// Publish all packet
        /// </summary>
        /// <param name="packet"></param>
        public void Publish(IPacket packet);

    }
}