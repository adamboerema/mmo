using System;
using System.Numerics;
using Common.Model.Shared;
using Common.Store;
using Server.Component.Player;

namespace Server.Engine.Player
{
    public interface IPlayerStore: IStore<string, PlayerComponent>
    {
        //public void UpdateMovement(
        //    string playerId,
        //    Vector3 coordinates,
        //    Direction direction,
        //    bool isMoving);
    }
}
