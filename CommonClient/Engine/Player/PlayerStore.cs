using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Common.Base;
using CommonClient.Engine.Player;

namespace CommonClient.Store
{
    public class PlayerStore: IPlayerStore
    {
        private ConcurrentDictionary<string, ClientPlayerModel> _players
            = new ConcurrentDictionary<string, ClientPlayerModel>();

        private string _clientPlayerId;

        public ClientPlayerModel Get(string playerId)
        {
            _players.TryGetValue(playerId, out var player);
            return player;
        }

        public void Add(ClientPlayerModel model)
        {
            // Store reference to client player id
            if(model.IsClient)
            {
                _clientPlayerId = model.Id;
            }
            _players[model.Id] = model;
        }

        public IDictionary<string, ClientPlayerModel> GetAll()
        {
            return _players;
        }

        public void Remove(string id)
        {
            _players.TryRemove(id, out _);
        }

        public void Update(ClientPlayerModel model)
        {
            _players.TryUpdate(model.Id, model, _players[model.Id]);
        }

        public ClientPlayerModel GetClientPlayer()
        {
            return Get(_clientPlayerId);
        }

        public void UpdateMovement(
            string playerId,
            Vector3 coordinates,
            Direction movementType,
            bool isMoving)
        {
            var player = Get(playerId);
            if(player != null)
            {
                player.Coordinates = coordinates;
                player.Direction = movementType;
                player.IsMoving = isMoving;
                Update(player);
            }
        }

        public void UpdateClientCoordinates(
            Vector3 coordinates,
            Direction movementType,
            bool isMoving)
        {
            UpdateMovement(_clientPlayerId, coordinates, movementType, isMoving);
        }

        public void UpdateClientMovement(Direction movementType, bool isMoving)
        {
            var clientPlayer = GetClientPlayer();
            if(clientPlayer != null)
            {
                clientPlayer.Direction = movementType;
                clientPlayer.IsMoving = isMoving;
                Update(clientPlayer);
            }
        }
    }
}
