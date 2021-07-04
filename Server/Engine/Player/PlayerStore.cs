using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;

namespace Server.Engine.Player
{
    public class PlayerStore: IPlayerStore
    {
        private Dictionary<string, PlayerModel> _players = new Dictionary<string, PlayerModel>();

        public void Add(PlayerModel model)
        {
            _players.Add(model.Id, model);
        }

        public PlayerModel Get(string id)
        {
            return _players.ContainsKey(id) ? _players[id] : null;
        }

        public ICollection<KeyValuePair<string, PlayerModel>> GetAll()
        {
            return _players;
        }

        public void Remove(string id)
        {
            _players.Remove(id);
        }

        public void Update(PlayerModel model)
        {
            _players[model.Id] = model;
        }

        public void UpdateMovement(string playerId, Vector3 coordinates, MovementType movementType)
        {
            var player = Get(playerId);
            if (player != null)
            {
                player.Character.Coordinates = coordinates;
                player.Character.MovementType = movementType;
                Update(player);
            }
        }

    }
}
