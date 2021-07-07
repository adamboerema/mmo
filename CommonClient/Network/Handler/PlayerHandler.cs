using System;
using Common.Packets.ServerToClient.Player;
using CommonClient.Engine.Player;

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
            Console.WriteLine($"Packet {packet}: {packet.Id}");
            _playerManager.InitializePlayer(packet.PlayerId);
        }

        public void Handle(PlayerDisconnectPacket packet)
        {
            Console.WriteLine($"Packet {packet}: {packet.Id}");
            _playerManager.RemovePlayer(packet.PlayerId);
        }
    }
}
