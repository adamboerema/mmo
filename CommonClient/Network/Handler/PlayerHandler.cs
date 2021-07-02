using System;
using Common.Packets.ServerToClient.Player;
using CommonClient.Player;

namespace CommonClient.Network.Handler
{
    public class PlayerHandler: IPacketHandler<PlayerConnectPacket>,
        IPacketHandler<PlayerDisconnectPacket>
    {
        private readonly IPlayerManager _playerManager;

        public PlayerHandler(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Handle(PlayerConnectPacket packet)
        {
            _playerManager.CreatePlayer(packet.PlayerId);
        }

        public void Handle(PlayerDisconnectPacket packet)
        {
            _playerManager.RemovePlayer(packet.PlayerId);
        }
    }
}
