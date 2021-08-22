using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Base;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager
    {
        public void InitializePlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            Direction movementType);

        public void UpdatePlayer(ClientPlayerModel playerModel);

        public void RemovePlayer(string playerId);

        public IEnumerable<ClientPlayerModel> GetPlayers();

        public ClientPlayerModel GetClientPlayer();

    }
}
