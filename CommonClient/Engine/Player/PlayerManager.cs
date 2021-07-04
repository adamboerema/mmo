using System;
using Common.Model;
using CommonClient.Store;

namespace CommonClient.Engine.Player
{
    public class PlayerManager: IPlayerManager
    {
        private IPlayersStore _playerStore;

        public PlayerManager(IPlayersStore playerStore)
        {
            _playerStore = playerStore;
        }

        public void CreateClientPlayer(string playerId)
        {
            var player = CreateNewPlayer(playerId, true);
            _playerStore.Add(player);
        }

        public void CreatePlayer(string playerId)
        {
            var player = CreateNewPlayer(playerId);
            _playerStore.Add(player);
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
        private ClientPlayerModel CreateNewPlayer(string id, bool isClient = false) =>
            new ClientPlayerModel
            {
                Id = id,
                IsClientPlayer = isClient,
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
