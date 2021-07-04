using System;
using System.Numerics;
using Common.Model;
using Common.Store;

namespace Server.Engine.Player
{
    public interface IPlayerStore: IStore<string, PlayerModel>
    {
        public void UpdateMovement(string playerId, Vector3 coordinates, MovementType movementType);
    }
}
