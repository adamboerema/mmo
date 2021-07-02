using System;
using Common.Bus;
using CommonClient.Bus.Packet;

namespace CommonClient.Network.Receiver
{
    public interface IConnectionReceiver: IEventBusListener<PacketEvent>
    {
    }
}
