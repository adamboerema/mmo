using System;
using System.Collections.Generic;
using Common.Model;
using CommonClient.Engine.Player;

namespace CommonClient.Store
{
    public class PlayerStore: IPlayerStore
    {
        private Dictionary<string, PlayerModel> _players = new Dictionary<string, PlayerModel>();

        public PlayerModel Get(string playerId)
        {
            return _players.ContainsKey(playerId) ? _players[playerId] : null;
        }

        public void Add(PlayerModel playerModel)
        {
            _players.Add(playerModel.Id, playerModel);
        }

        public void Remove(string playerId)
        {
            _players.Remove(playerId);
        }

        public void Update(PlayerModel playerModel)
        {
            _players[playerModel.Id] = playerModel;
        }

        public void UpdateMovement(string playerId, int x, int y, int z, MovementType movementType)
        {
            var player = Get(playerId);
            if(player != null)
            {
                player.Character.X = x;
                player.Character.Y = y;
                player.Character.Z = z;
                player.Character.MovementType = movementType;
                Update(player);
            }
        }
    }
}
