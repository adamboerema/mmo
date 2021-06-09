using System;
using System.Collections.Generic;

namespace Server.Bus
{
    public abstract class EventBus<T> : IEventBus<T>
    {
        private readonly IList<IEventBusListener<T>> listeners = new List<IEventBusListener<T>>();

        public void Subscribe(IEventBusListener<T> listener)
        {
            listeners.Add(listener);
        }

        public void Unsubscribe(IEventBusListener<T> listener)
        {
            listeners.Remove(listener);
        }

        public void Publish(T eventObject)
        {
            foreach(var listener in listeners)
            {
                listener?.Handle(eventObject);
            }
        }
    }
}
