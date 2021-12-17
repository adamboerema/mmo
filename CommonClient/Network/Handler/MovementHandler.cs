using System;
using Common.Packets.ServerToClient.Player;
using CommonClient.Engine.Player;

namespace CommonClient.Network.Handler
{
    public class MovementHandler: IPacketHandler<PlayerMovementOutputPacket>
    {
        public IPlayerManager _movementManager;

        public MovementHandler(IPlayerManager movementManager)
        {
            _movementManager = movementManager;
        }

        public void Handle(PlayerMovementOutputPacket packet)
        {
            Console.WriteLine($"Movement Packet: {packet.Position} {packet.MovementType}");
            _movementManager.UpdatePlayerCoordinatesOutput(
                packet.PlayerId,
                packet.Position,
                packet.MovementType,
                packet.IsMoving);
        }
    }
}
