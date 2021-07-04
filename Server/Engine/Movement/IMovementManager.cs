using System;
using Common.Model;

namespace Server.Engine.Movement
{
    public interface IMovementManager
    {
        public void UpdateMovementInput(string playerId, MovementType movementType);
    }
}
