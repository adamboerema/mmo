using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Network.Definitions;
using Common.Network.Packets.Auth;
using Server.Bus.Packet;
using Server.Network.Handler;

namespace Server.Network.Connection
{
    public class ConnectionReceiver : IConnectionReceiver, IEventBusListener<ReceiverPacketEvent>
    {
        private readonly IReceiverPacketBus _packetBus;
        private readonly Dictionary<Type, IPacketHandler<IPacket>> _handlers;

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

        public void Receive(string connectionId, IPacket receivePacket)
        {
            switch(receivePacket)
            {
                case LoginResponsePacket packet:
                    Handle(connectionId, packet);
                    break;
            }
            Handle(connectionId, receivePacket);
        }

        private void Handle<T>(string connectionId, T receivePacket) where T: IPacket
        {
            var handler = _serviceProvider.GetService(typeof(IPacketHandler<T>)) as IPacketHandler<T>;
            handler?.Handle(connectionId, receivePacket);
        }
    }
}
