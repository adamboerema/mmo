using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Common.Model.Shared;
using CommonClient.Engine.Player;

namespace CommonClient.Store
{
    public class PlayerStore: IPlayerStore
    {
        private ConcurrentDictionary<string, ClientPlayerEntity> _players
            = new ConcurrentDictionary<string, ClientPlayerEntity>();

        private string _clientPlayerId;

        public ClientPlayerEntity Get(string playerId)
        {
            _players.TryGetValue(playerId, out var player);
            return player;
        }

        public void Add(ClientPlayerEntity model)
        {
            // Store reference to client player id
            if(model.IsClient)
            {
                _clientPlayerId = model.Id;
            }
            _players[model.Id] = model;
        }

        public IDictionary<string, ClientPlayerEntity> GetAll()
        {
            return _players;
        }

        public void Remove(string id)
        {
            _players.TryRemove(id, out _);
        }

        public void Update(ClientPlayerEntity model)
        {
            _players.TryUpdate(model.Id, model, _players[model.Id]);
        }

        public ClientPlayerEntity GetClientPlayer()
        {
            return Get(_clientPlayerId);
        }

        public void UpdateMovement(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            var player = Get(playerId);
            if(player != null)
            {
                player.UpdateCoordinates(coordinates, direction, isMoving);
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

        public void UpdateClientMovement(Direction direction, bool isMoving)
        {
            var clientPlayer = GetClientPlayer();
            if(clientPlayer != null)
            {
                clientPlayer.UpdateDirection(direction, isMoving);
                Update(clientPlayer);
            }
        }
    }
}
