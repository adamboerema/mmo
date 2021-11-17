using System;
using Common.Component;
using Server.Component.Player;
using Common.Model.Shared;

namespace Server.Engine.Player
{
    public interface IPlayerManager: IEngineComponent
    {
        /// <summary>
        /// Add Player to the player pool
        /// </summary>
        /// <param name="player">Player object</param>
        public void AddPlayer(PlayerComponent player);

        /// <summary>
        /// Creates a new player and adds it to the store
        /// </summary>
        /// <param name="connectionId"></param>
        public void InitializePlayer(string connectionId);

        /// <summary>
        /// Remove player from pool
        /// </summary>
        /// <param name="connectionId">Connection id associated with player</param>
        public void RemovePlayer(string connectionId);


        /// <summary>
        /// Update Movement state of player
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="movementType"></param>
        /// <param name="isMoving"></param>
        public void UpdateMovementInput(
            string playerId,
            Direction movementType,
            bool isMoving);
    }
}
