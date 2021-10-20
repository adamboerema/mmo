using System;
using Common.Base;
using Common.Model.Character;

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
