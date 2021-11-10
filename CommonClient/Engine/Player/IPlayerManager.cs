using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Component;
using Common.Model.Shared;

namespace CommonClient.Engine.Player
{
    public interface IPlayerManager: IEngineComponent
    {
        public void InitializePlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            Direction movementType);

        public void UpdateClientMovementInput(
            Direction direction,
            bool isMoving);

        public void UpdatePlayerCoordinatesOutput(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving);

        public void UpdatePlayer(ClientPlayerEntity playerModel);

        public void RemovePlayer(string playerId);

        public IEnumerable<ClientPlayerEntity> GetPlayers();

        public ClientPlayerEntity GetClientPlayer();

    }
}
