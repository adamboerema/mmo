using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Network.Packet.Definitions;

namespace Server.Bus.Packet
{
    public class DispatchPacketBus : IDispatchPacketBus
    {
        private readonly IList<IEventBusListener<DispatchPacketEvent>> listeners
            = new List<IEventBusListener<DispatchPacketEvent>>();

        public void Publish(string connectionId, IPacket packet, DispatchType dispatchType)
        {
            Publish(new DispatchPacketEvent
            {
                Type = dispatchType,
                ConnectionId = connectionId,
                Packet = packet
            });
        }

        public void Publish(DispatchPacketEvent eventObject)
        {
            foreach (var listener in listeners)
            {
                listener.Handle(eventObject);
            }
        }

        public void Subscribe(IEventBusListener<DispatchPacketEvent> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(IEventBusListener<DispatchPacketEvent> listener)
        {
            listeners.Remove(listener);
        }
    }
}
