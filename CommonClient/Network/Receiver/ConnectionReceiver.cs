using System;
using Common.Bus;
using CommonClient.Bus.Packet;
using CommonClient.Container;
using Common.Definitions;

namespace CommonClient.Network.Receiver
{
    public class ConnectionReceiver: IConnectionReceiver, IEventBusListener<PacketEvent>
    {
        private IReceiverPacketBus _receiverPacketBus;

        public ConnectionReceiver()
        {
            _receiverPacketBus = GameServices.GetService<IReceiverPacketBus>();
            _receiverPacketBus.Subscribe(this);
        }

        public void Handle(PacketEvent eventObject)
        {
            switch(eventObject.Packet.Id)
            {
                case PacketType.LOGIN_RESPONSE:
                    break;
                case PacketType.PLAYER_CONNECTED:
                    break;
                case PacketType.PLAYER_DISCONNECTED:
                    break;
                case PacketType.MOVEMENT_OUTPUT:
                    break;
            }
        }
    }
}
