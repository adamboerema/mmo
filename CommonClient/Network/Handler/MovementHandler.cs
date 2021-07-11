using System;
using System.Numerics;
using Common.Packets.ServerToClient.Movement;
using CommonClient.Engine.Movement;

namespace CommonClient.Network.Handler
{
    public class MovementHandler: IPacketHandler<MovementOutputPacket>
    {
        public IMovementManager _movementManager;

        public MovementHandler(IMovementManager movementManager)
        {
            _movementManager = movementManager;
        }

        public void Handle(MovementOutputPacket packet)
        {
            Console.WriteLine($"Movement Packet: {packet.Position} {packet.MovementType}");
            _movementManager.UpdatePlayerCoordinates(
                packet.PlayerId,
                packet.Position,
                packet.MovementType);
        }
    }
}
