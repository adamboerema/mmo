using System;
using Common.Base;

namespace Server.Engine.Player
{
    public interface IPlayerComponent: IGameComponent
    {
        /// <summary>
        /// Add Player to the player pool
        /// </summary>
        /// <param name="player">Player object</param>
        public void AddPlayer(PlayerModel player);

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
    }
}
