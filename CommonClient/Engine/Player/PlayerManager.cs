using System;
using Common.Model;
using CommonClient.Store;

namespace CommonClient.Engine.Player
{
    public class PlayerManager: IPlayerManager
    {
        private IStore<string, PlayerModel> _playerStore;

        public PlayerManager(IStore<string, PlayerModel> playerStore)
        {
            _playerStore = playerStore;
        }

        public void CreatePlayer(string playerId)
        {
            var player = CreateNewPlayer(playerId);
            _playerStore.Add(playerId, player);
        }

        public void RemovePlayer(string playerId)
        {
            _playerStore.Remove(playerId);
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
