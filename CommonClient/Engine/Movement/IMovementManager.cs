using System;
using Common.Model;

namespace CommonClient.Engine.Movement
{
    public interface IMovementManager
    {
        public void DispatchMovementInput(MovementType movementType);

        public void ReceiveUpdateMovement(string playerId, int x, int y, int z, MovementType movementType);
    }
}
