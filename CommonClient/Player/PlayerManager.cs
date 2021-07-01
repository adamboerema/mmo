using System;
using System.Collections.Generic;
using Common.Bus;
using CommonClient.Bus.Packet;
using CommonClient.Container;
using Common.Model;
using Common.Network.Packets.Player;

namespace CommonClient.Player
{
    public class PlayerManager: IPlayerManager, IEventBusListener<PacketEvent>
    {
        private Dictionary<string, PlayerModel> _players = new Dictionary<string, PlayerModel>();
        private IReceiverPacketBus _receiverPacketBus;

        public PlayerManager()
        {
            _receiverPacketBus = GameServices.GetService<IReceiverPacketBus>();
            _receiverPacketBus.Subscribe(this);
        }

        public void Handle(PacketEvent eventObject)
        {
            switch(eventObject.Packet)
            {
                case PlayerConnectPacket packet:
                    var player = CreateNewPlayer(packet.PlayerId);
                    _players.Add(packet.PlayerId, player);
                    break;
                case PlayerDisconnectPacket packet:
                    _players.Remove(packet.PlayerId);
                    break;
            }
        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private PlayerModel CreateNewPlayer(string id) => new PlayerModel
        {
            Id = id,
            Character = new CharacterModel
            {
                Name = "test",
                X = 0,
                Y = 0,
                Z = 0
            }
        };
    }
}
