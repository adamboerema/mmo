using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;
using CommonClient.Engine.Player;

namespace CommonClient.Store
{
    public class PlayersStore: IPlayersStore
    {
        private Dictionary<string, ClientPlayerModel> _players = new Dictionary<string, ClientPlayerModel>();

        public ClientPlayerModel Get(string playerId)
        {
            return _players.ContainsKey(playerId) ? _players[playerId] : null;
        }

        public void Add(ClientPlayerModel playerModel)
        {
            _players.Add(playerModel.Id, playerModel);
        }

        public ICollection<KeyValuePair<string, ClientPlayerModel>> GetAll()
        {
            return _players;
        }

        public void Remove(string playerId)
        {
            _players.Remove(playerId);
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
    }
}
