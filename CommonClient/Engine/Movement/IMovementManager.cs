using System;
using System.Numerics;
using Common.Model;

namespace CommonClient.Engine.Movement
{
    public interface IMovementManager
    {
        public void UpdateClientMovementInput(MovementType movementType);

        public void UpdatePlayerCoordinates(string playerId, Vector3 coordinates, MovementType movementType);
    }
}
