using System;
using Common.Model;

namespace Server.Engine.Movement
{
    public interface IMovementManager : IGameLoopEvent
    {
        public void UpdateMovementInput(string playerId, MovementType movementType);
    }
}
