using System;
using System.Numerics;
using Common.Base;
using Common.Store;

namespace CommonClient.Engine.Player
{
    public interface IPlayerStore: IStore<string, ClientPlayerModel>
    {
        public void UpdateMovement(string playerId, Vector3 coordinates, MovementType movementType);

        public ClientPlayerModel GetClientPlayer();

        public void UpdateClientCoordinates(Vector3 coordinates, MovementType movementType);

        public void UpdateClientMovementType(MovementType movementType);
    }
}
