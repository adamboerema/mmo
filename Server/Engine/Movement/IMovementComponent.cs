using System;
using Common.Base;

namespace Server.Engine.Movement
{
    public interface IMovementComponent : IGameComponent
    {
        public void UpdateMovementInput(
            string playerId,
            Direction movementType,
            bool isMoving);
    }
}
