using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager
    {
        public void InitializePlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            MovementType movementType);

        public void RemovePlayer(string playerId);

        public ICollection<KeyValuePair<string, ClientPlayerModel>> GetPlayers();

    }
}
