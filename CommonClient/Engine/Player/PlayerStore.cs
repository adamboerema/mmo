using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;
using CommonClient.Engine.Player;

namespace CommonClient.Store
{
    public class PlayersStore: IPlayerStore
    {
        private ConcurrentDictionary<string, ClientPlayerModel> _players
            = new ConcurrentDictionary<string, ClientPlayerModel>();

        private string _clientPlayerId;

        public ClientPlayerModel Get(string playerId)
        {
            return _players.ContainsKey(playerId) ? _players[playerId] : null;
        }

        public void Add(ClientPlayerModel playerModel)
        {
            // Store reference to client player id
            if(playerModel.IsClient)
            {
                _clientPlayerId = playerModel.Id;
            }
            _players[playerModel.Id] = playerModel;
        }

        public ICollection<KeyValuePair<string, ClientPlayerModel>> GetAll()
        {
            return _players;
        }

        public void Remove(string playerId)
        {
            _players.TryRemove(playerId, out var playerModel);
        }

        public void Update(ClientPlayerModel playerModel)
        {
            _players[playerModel.Id] = playerModel;
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

        public void SetClientPlayer(string playerId)
        {
            _clientPlayerId = playerId;
        }

        public ClientPlayerModel GetClientPlayer()
        {
            return _players[_clientPlayerId];
        }
    }
}
