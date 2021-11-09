using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Definitions;

namespace Server.Bus.Packet
{
    public class DispatchPacketBus : IDispatchPacketBus
    {
        private readonly HashSet<IEventBusListener<DispatchPacketEvent>> listeners
            = new HashSet<IEventBusListener<DispatchPacketEvent>>();

        public void Publish(string connectionId, IPacket packet)
        {
            Publish(new DispatchPacketEvent
            {
                Type = DispatchType.CONNECTION,
                ConnectionId = connectionId,
                Packet = packet
            });
        }

        public void PublishAll(IPacket packet)
        {
            Publish(new DispatchPacketEvent
            {
                Type = DispatchType.ALL,
                Packet = packet
            });
        }

        public void PublishExcept(string connectionId, IPacket packet)
        {
            Publish(new DispatchPacketEvent
            {
                Type = DispatchType.ALL_EXCEPT,
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
