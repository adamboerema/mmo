using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Definitions;

namespace Server.Bus.Packet
{
    public class ReceiverPacketBus: IReceiverPacketBus
    {
        private readonly HashSet<IEventBusListener<ReceiverPacketEvent>> listeners
            = new HashSet<IEventBusListener<ReceiverPacketEvent>>();

        public void Publish(ReceiverPacketEvent eventObject)
        {
            foreach(var listener in listeners)
            {
                listener.Handle(eventObject);
            }
        }

        public void Publish(string connectionId, IPacket packetEvent)
        {
            Publish(new ReceiverPacketEvent
            {
                ConnectionId = connectionId,
                Packet = packetEvent
            });
        }

        public void Subscribe(IEventBusListener<ReceiverPacketEvent> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(IEventBusListener<ReceiverPacketEvent> listener)
        {
            listeners.Remove(listener);
        }
    }
}
