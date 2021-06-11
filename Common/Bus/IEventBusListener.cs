using System;
namespace Common.Bus
{
    public interface IEventBusListener<T>
    {
        public void Handle(T eventObject);
    }
}
