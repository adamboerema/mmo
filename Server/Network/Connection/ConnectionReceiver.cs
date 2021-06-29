using System;
using Common.Bus;
using Common.Network.Definitions;
using Server.Bus.Packet;

namespace Server.Network.Connection
{
    public class ConnectionReceiver : IConnectionReceiver, IEventBusListener<ReceiverPacketEvent>
    {
        private readonly IReceiverPacketBus _packetBus;

        public ConnectionReceiver(IReceiverPacketBus packetBus)
        {
            _packetBus = packetBus;
            _packetBus.Subscribe(this);
        }

        public void Close()
        {
            _packetBus.Unsubscribe(this);
        }

        public void Handle(ReceiverPacketEvent eventObject)
        {
            Receive(eventObject.ConnectionId, eventObject.Packet);
        }

        public void Receive(string connectionId, IPacket packet)
        {
        }

    }
}
