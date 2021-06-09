using System;
using System.Collections.Generic;
using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public class DispatchPacketBus : IDispatchPacketBus
    {
        private readonly IList<IEventBusListener<PacketEvent>> listeners = new List<IEventBusListener<PacketEvent>>();

        public void Publish(string connectionId, PacketEvent packet)
        {
            Publish(new PacketEvent
            {
                ConnectionId = connectionId,
                Packet = packet.Packet
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
