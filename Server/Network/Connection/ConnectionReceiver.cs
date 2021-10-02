using System;
using Common.Bus;
using Common.Definitions;
using Server.Bus.Packet;
using Server.Network.Router;

namespace Server.Network.Connection
{
    public class ConnectionReceiver : IConnectionReceiver, IEventBusListener<ReceiverPacketEvent>
    {
        private readonly IReceiverPacketBus _packetBus;
        private readonly IHandlerRouter _handlerRouter;

        public ConnectionReceiver(IReceiverPacketBus packetBus,
            IHandlerRouter handlerRouter)
        {
            _handlerRouter = handlerRouter;
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

        public void Receive(string connectionId, IPacket receivePacket)
        {
            _handlerRouter.Route(connectionId, receivePacket);
        }
    }
}
