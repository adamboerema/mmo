using System;
using System.Numerics;
using Common.Entity;
using Common.Model.Shared;
using Common.Store;

namespace Server.Engine.Player
{
    public interface IPlayerStore: IStore<string, PlayerEntity>
    {
        public void UpdateMovement(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving);
    }
}
