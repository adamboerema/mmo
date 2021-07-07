using System;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager
    {
        public void InitializePlayer(string playerId);

        public void AddPlayer(string playerId);

        public void RemovePlayer(string playerId);
    }
}
