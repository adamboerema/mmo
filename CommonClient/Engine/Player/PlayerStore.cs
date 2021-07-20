using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;
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
            return _players.ContainsKey(playerId) ? _players[playerId] : null;
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

        public void UpdateMovement(string playerId, Vector3 coordinates, MovementType movementType)
        {
            var player = Get(playerId);
            if(player != null)
            {
                player.Character.Coordinates = coordinates;
                player.Character.MovementType = movementType;
                Update(player);
            }
        }

        public ClientPlayerModel GetClientPlayer()
        {
            return Get(_clientPlayerId);
        }

        public void UpdateClientCoordinates(Vector3 coordinates, MovementType movementType)
        {
            UpdateMovement(_clientPlayerId, coordinates, movementType);
        }

        public void UpdateClientMovementType(MovementType movementType)
        {
            var clientPlayer = GetClientPlayer();
            if(clientPlayer != null)
            {
                clientPlayer.Character.MovementType = movementType;
                Update(clientPlayer);
            }
        }
    }
}
