using System;
using System.Numerics;
using Common.Packets.ServerToClient.Movement;
using CommonClient.Engine.Movement;

namespace CommonClient.Network.Handler
{
    public class MovementHandler: IPacketHandler<MovementOutputPacket>
    {
        public IPlayerManager _movementManager;

        public MovementHandler(IPlayerManager movementManager)
        {
            _movementManager = movementManager;
        }

        public void Handle(MovementOutputPacket packet)
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
