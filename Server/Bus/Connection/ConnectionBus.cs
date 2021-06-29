using System;
using System.Collections.Generic;
using Common.Bus;

namespace Server.Bus.Connection
{
    public class ConnectionBus : IConnectionBus
    {
        private readonly IList<IEventBusListener<ConnectionEvent>> listeners
            = new List<IEventBusListener<ConnectionEvent>>();

        public void Publish(string connectionId, ConnectionState state)
        {
            Publish(new ConnectionEvent
            {
                Id = connectionId,
                State = state
            });
        }

        public void Publish(ConnectionEvent eventObject)
        {
            foreach (var listener in listeners)
            {
                listener.Handle(eventObject);
            }
        }

        public void Subscribe(IEventBusListener<ConnectionEvent> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(IEventBusListener<ConnectionEvent> listener)
        {
            listeners.Remove(listener);
        }
    }
}
