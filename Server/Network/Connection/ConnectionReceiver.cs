using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Network.Definitions;
using Common.Network.Packets.Auth;
using Server.Bus.Packet;
using Server.Network.Handler;
using Server.Network.Handler.Factory;

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
