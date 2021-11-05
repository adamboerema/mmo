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

        /// <summary>
        /// Dispatch player connect
        /// </summary>
        /// <param name="connectionId"></param>
        /// <param name="playerId"></param>
        /// <param name="isClient"></param>
        /// <param name="isMoving"></param>
        /// <param name="position"></param>
        /// <param name="direction"></param>
        public void DispatchPlayerConnect(
            string connectionId,
            string playerId,
            bool isClient,
            bool isMoving,
            Vector3 position,
            Direction direction);


        /// <summary>
        /// Dispatch player disconnect to all
        /// </summary>
        /// <param name="playerId"></param>
        public void DispatchPlayerDisconnect(string playerId);
    }
}
