using System;
using Common.Model.Shared;

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
