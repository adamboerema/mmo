﻿using System;
using System.Numerics;
using Common.Model;
using Common.Store;

namespace CommonClient.Engine.Player
{
    public interface IPlayersStore: IStore<string, ClientPlayerModel>
    {
        public void UpdateMovement(string playerId, Vector3 coordinates, MovementType movementType);
    }
}
