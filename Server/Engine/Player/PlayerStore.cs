using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Numerics;
using Common.Entity;
using Common.Model.Shared;
using Server.Component.Player;

namespace Server.Engine.Player
{
    public class PlayerStore: IPlayerStore
    {

        private ConcurrentDictionary<string, PlayerComponent> _players
            = new ConcurrentDictionary<string, PlayerComponent>();

        public void Add(PlayerComponent component)
        {
            _players[component.Id] = component;
        }

        public PlayerComponent Get(string id)
        {
            return _players.ContainsKey(id) ? _players[id] : null;
        }

        public IDictionary<string, PlayerComponent> GetAll()
        {
            return _players;
        }

        public void Remove(string id)
        {
            _players.TryRemove(id, out _);
        }

        public void Update(PlayerComponent model)
        {
            _players.TryUpdate(model.Id, model, _players[model.Id]);
        }

        public void UpdateMovement(
            string playerId,
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            var player = Get(playerId);
            if (player != null)
            {
                player.UpdateCoordinates(coordinates, direction, isMoving);
                Update(player);
            }
        }

    }
}
