using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Definitions;

namespace CommonClient.Bus.Packet
{
    public class ReceiverPacketBus: IEventBus<PacketEvent>, IReceiverPacketBus
    {
        private readonly IList<IEventBusListener<PacketEvent>> listeners = new List<IEventBusListener<PacketEvent>>();

        public void Publish(IPacket packet)
        {
            Publish(new PacketEvent
            {
                Packet = packet
            });
        }

        public void Publish(PacketEvent eventObject)
        {
            foreach (var listener in listeners)
            {
                listener.Handle(eventObject);
            }
        }

        public void Subscribe(IEventBusListener<PacketEvent> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(IEventBusListener<PacketEvent> listener)
        {
            listeners.Remove(listener);
        }
    }
}
