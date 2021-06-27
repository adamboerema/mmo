using System;
using System.Collections.Generic;
using Common.Bus;
using CommonClient.Bus.Packet;
using CommonClient.Container;
using Common.Network.Packet.Definitions;

namespace CommonClient.Player
{
    public class PlayerManager: IPlayerManager, IEventBusListener<PacketEvent>
    {
        private Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private IReceiverPacketBus _receiverPacketBus;

        public PlayerManager()
        {
            _receiverPacketBus = GameServices.GetService<IReceiverPacketBus>();
            _receiverPacketBus.Subscribe(this);
        }

        public void Handle(PacketEvent eventObject)
        {
            switch(eventObject.Packet.Id)
            {
                case PacketType.PLAYER_CONNECTED:
                    break;
                case PacketType.PLAYER_DISCONNECTED:
                    break;
            }
        }
    }
}
