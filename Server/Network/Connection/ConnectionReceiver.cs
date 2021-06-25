using System;
using Common.Bus;
using Common.Network.Packet.Definitions;
using Common.Network.Packet.Definitions.Schema.Auth;
using Server.Bus;
using Server.Bus.Packet;
using Server.Network.Handler;

namespace Server.Network.Connection
{
    public class ConnectionReceiver: IConnectionReceiver, IEventBusListener<ReceiverPacketEvent>
    {
        private readonly IReceiverPacketBus _packetBus;
        private readonly AuthHandler _authHandler;

        public ConnectionReceiver(
            IReceiverPacketBus packetBus,
            AuthHandler authHandler)
        {
            _packetBus = packetBus;
            _authHandler = authHandler;
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
            switch(packet.Id)
            {
                case Definitions.LOGIN_REQUEST:
                    _authHandler.Handle(connectionId, packet as LoginRequestPacket);
                    break;
            }
        }

    }
}
