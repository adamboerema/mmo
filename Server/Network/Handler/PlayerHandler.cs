using System;
using Common.Packets.ClientToServer.Movement;
using Server.Engine.Player;

namespace Server.Network.Handler
{
    public class PlayerHandler: IPacketHandler<PlayerMovementPacket>
    {
        private readonly IPlayerManager _playerManager;

        public PlayerHandler(IPlayerManager playerManager)
        {
            _playerManager = playerManager;
        }

        public void Handle(string connectionId, PlayerMovementPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Movement Packet { packet.Direction }");
            _playerManager.UpdateMovementInput(
                connectionId,
                packet.Direction,
                packet.IsMoving);
        }
    }
}
