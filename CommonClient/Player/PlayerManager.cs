using System;
using System.Collections.Generic;
using Common.Model;

namespace CommonClient.Player
{
    public class PlayerManager: IPlayerManager
    {
        private Dictionary<string, PlayerModel> _players = new Dictionary<string, PlayerModel>();

        public PlayerManager()
        {
        }

        public void CreatePlayer(string playerId)
        {
            var player = CreateNewPlayer(playerId);
            _players.Add(playerId, player);
        }

        public void RemovePlayer(string playerId)
        {
            _players.Remove(playerId);
        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private PlayerModel CreateNewPlayer(string id) => new PlayerModel
        {
            Id = id,
            Character = new CharacterModel
            {
                Name = "test",
                X = 0,
                Y = 0,
                Z = 0
            }
        };
    }
}
