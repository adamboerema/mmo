﻿using System;
using Common.Base;

namespace Server.Engine.Movement
{
    public interface IMovementManager : IGameLoopEvent
    {
        public void UpdateMovementInput(
            string playerId,
            Direction movementType,
            bool isMoving);
    }
}
