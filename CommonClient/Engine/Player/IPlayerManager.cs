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
        public PlayerComponent GetClientPlayer();

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

        public void RemovePlayer(string playerId);

    }
}
