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
            Console.WriteLine($"Movement Packet: {packet.X} {packet.Y} {packet.Z} {packet.MovementType}");
            _movementManager.UpdatePlayerCoordinates(
                packet.PlayerId,
                new Vector3(packet.X, packet.Y, packet.Z),
                packet.MovementType);
        }
    }
}
