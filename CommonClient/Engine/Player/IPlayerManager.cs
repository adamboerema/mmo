using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model.Shared;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager
    {
        public void InitializePlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            Direction movementType);

        public void UpdatePlayer(ClientPlayerEntity playerModel);

        public void RemovePlayer(string playerId);

        public IEnumerable<ClientPlayerEntity> GetPlayers();

        public ClientPlayerEntity GetClientPlayer();

    }
}
