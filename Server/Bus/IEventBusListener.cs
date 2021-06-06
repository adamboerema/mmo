using System;
namespace Server.Bus
{
    public interface IEventBusListener<T>
    {
        public void Handle(T eventObject);
    }
}
