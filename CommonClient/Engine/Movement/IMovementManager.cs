﻿using System;
using System.Numerics;
using Common.Base;

namespace CommonClient.Engine.Movement
{
    public interface IMovementManager
    {
        public void UpdateClientMovementInput(Direction direction, bool isMoving);

        public void UpdatePlayerCoordinatesOutput(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving);
    }
}
