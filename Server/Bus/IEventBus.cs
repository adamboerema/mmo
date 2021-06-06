using System;
namespace Server.Bus
{
    public interface IEventBus<T>
    {
        public void Subscribe(IEventBusListener<T> listener);

        public void Unsubscribe(IEventBusListener<T> listener);

        public void Publish(T eventObject);
    }
}
