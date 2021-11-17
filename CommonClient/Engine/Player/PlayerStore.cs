using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Common.Model.Shared;
using Common.Store;
using CommonClient.ComponentStore.Player;
using CommonClient.Engine.Player;

namespace CommonClient.Store
{
    public class PlayerStore: ComponentStore<PlayerComponent>
    {

        private string _clientPlayerId;

        public override void Add(PlayerComponent component)
        {
            // Store reference to client player id
            if(component.IsClient)
            {
                _clientPlayerId = component.Id;
            }
            base.Add(component);
        }

        public PlayerComponent GetClientPlayer()
        {
            return base.Get(_clientPlayerId);
        }

        //public void UpdateMovement(
        //    string playerId,
        //    Vector3 coordinates,
        //    Direction direction,
        //    bool isMoving)
        //{
        //    var player = Get(playerId);
        //    if(player != null)
        //    {
        //        player.UpdateCoordinates(coordinates, direction, isMoving);
        //        Update(player);
        //    }
        //}

        //public void UpdateClientCoordinates(
        //    Vector3 coordinates,
        //    Direction movementType,
        //    bool isMoving)
        //{
        //    UpdateMovement(_clientPlayerId, coordinates, movementType, isMoving);
        //}

        //public void UpdateClientMovement(Direction direction, bool isMoving)
        //{
        //    var clientPlayer = GetClientPlayer();
        //    if(clientPlayer != null)
        //    {
        //        clientPlayer.UpdateDirection(direction, isMoving);
        //        Update(clientPlayer);
        //    }
        //}
    }
}
