using System;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager
    {
        public void CreatePlayer(string playerId);

        public void RemovePlayer(string playerId);
    }
}
