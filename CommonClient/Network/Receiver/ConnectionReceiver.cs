using System;
using CommonClient.Bus.Packet;
using CommonClient.Network.Handler.Router;

namespace CommonClient.Network.Receiver
{
    public class ConnectionReceiver: IConnectionReceiver
    {
        private readonly IReceiverPacketBus _receiverPacketBus;
        private readonly IHandlerRouter _handlerRouter;

        public ConnectionReceiver(IReceiverPacketBus receiverPacketBus,
            IHandlerRouter handlerRouter)
        {
            _handlerRouter = handlerRouter;
            _receiverPacketBus = receiverPacketBus;
            _receiverPacketBus.Subscribe(this);
        }

        public void Handle(PacketEvent eventObject)
        {
            _handlerRouter.Route(eventObject.Packet);
        }

        public void Close()
        {
            _receiverPacketBus.Unsubscribe(this);
        }

    }
}
