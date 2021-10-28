using System;
using System.Numerics;
using Common.Model.Shared;

namespace Server.Network.Dispatch
{
    public interface IPlayerDispatch
    {
        /// <summary>
        /// Dispatch the movement update to all of the clients
        /// </summary>
        /// <param name="player"></param>
        public void DispatchMovementUpdate(
           string playerId,
           Vector3 coordinates,
           Direction direction,
           bool isMoving);
    }
}
