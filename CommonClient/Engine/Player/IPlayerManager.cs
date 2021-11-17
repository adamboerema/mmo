using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Component;
using Common.Model.Shared;
using CommonClient.ComponentStore.Player;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager: IEngineComponent
    {
        /// <summary>
        /// Get the current client player
        /// </summary>
        /// <returns></returns>
        public PlayerComponent GetClientPlayer();

        /// <summary>
        /// Player connected
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="isClient"></param>
        /// <param name="position"></param>
        /// <param name="movementType"></param>
        public void InitializePlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            Direction movementType);

        /// <summary>
        /// Update the movement input of the client player
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="isMoving"></param>
        public void UpdateClientMovementInput(
            Direction direction,
            bool isMoving);

        /// <summary>
        /// Update a Player coordinates 
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="coordinates"></param>
        /// <param name="direction"></param>
        /// <param name="isMoving"></param>
        public void UpdatePlayerCoordinatesOutput(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving);

        /// <summary>
        /// Remove a player
        /// </summary>
        /// <param name="playerId"></param>
        public void RemovePlayer(string playerId);

    }
}
