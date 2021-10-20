using System;
using System.Numerics;
using Common.Base;
using Common.Model.Shared;
using Common.Store;

namespace CommonClient.Engine.Player
{
    public interface IPlayerStore: IStore<string, ClientPlayerModel>
    {
        /// <summary>
        /// Update the movement
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="coordinates"></param>
        /// <param name="movementType"></param>
        /// <param name="isMoving"></param>
        public void UpdateMovement(
            string playerId,
            Vector3 coordinates,
            Direction movementType,
            bool isMoving);

        /// <summary>
        /// Get Client player
        /// </summary>
        /// <returns></returns>
        public ClientPlayerModel GetClientPlayer();

        /// <summary>
        /// Update the client coordinates
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="movementType"></param>
        /// <param name="isMoving"></param>
        public void UpdateClientCoordinates(
            Vector3 coordinates,
            Direction movementType,
            bool isMoving);

        /// <summary>
        /// Update the client movement
        /// </summary>
        /// <param name="movementType"></param>
        /// <param name="isMoving"></param>
        public void UpdateClientMovement(Direction movementType, bool isMoving);
    }
}
