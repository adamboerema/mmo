using System;
using Common.Packets.ClientToServer.Movement;
using Server.Engine.Movement;

namespace Server.Network.Handler
{
    public class MovementHandler: IPacketHandler<MovementInputPacket>
    {
        private readonly IMovementManager _movementManager;

        public MovementHandler(IMovementManager movementManager)
        {
            _movementManager = movementManager;
        }

        public void Handle(string connectionId, MovementInputPacket packet)
        {
            Console.WriteLine($"Connection id: {connectionId}");
            Console.WriteLine($"Movement Packet { packet.Direction }");
            _movementManager.UpdateMovementInput(
                connectionId,
                packet.Direction,
                packet.IsMoving);
        }
    }
}
