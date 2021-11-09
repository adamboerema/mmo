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
        /// <param name="connectionId">Connection id to the current player</param>
        /// <param name="playerId">Player id connecting</param>
        /// <param name="isMoving">Is Player moving</param>
        /// <param name="position">Vector position of player</param>
        /// <param name="direction">Direction of player</param>
        public void DispatchPlayerConnect(
            string connectionId,
            string playerId,
            bool isMoving,
            Vector3 position,
            Direction direction);

        /// <summary>
        /// Dispatch player connect
        /// </summary>
        /// <param name="connectionId">Connection id to the current player</param>
        /// <param name="isMoving">Is Player moving</param>
        /// <param name="position">Vector position of player</param>
        /// <param name="direction">Direction of player</param>
        public void DispatchClientConnect(
            string connectionId,
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
