using System;
namespace Server.Engine.Player
{
    public interface IPlayerManager
    {
        /// <summary>
        /// Add Player to the player pool
        /// </summary>
        /// <param name="player">Player object</param>
        public void AddPlayer(Player player);

        /// <summary>
        /// Remove player from pool
        /// </summary>
        /// <param name="connectionId">Connection id associated with player</param>
        public void RemovePlayer(string connectionId);
    }
}
