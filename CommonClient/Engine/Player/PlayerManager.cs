using System;
using System.Numerics;
using Common.Model;

namespace CommonClient.Engine.Player
{
    public class PlayerManager: IPlayerManager
    {
        private IPlayerStore _playerStore;

        public PlayerManager(IPlayerStore playerStore)
        {
            _playerStore = playerStore;
        }

        public void InitializePlayer(string playerId)
        {
            _playerStore.SetClientPlayer(playerId);
        }

        public void AddPlayer(string playerId)
        {
            var player = CreateNewPlayer(playerId, true);
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
                    Coordinates = new Vector3(0, 0, 0)
                }
            };
    }
}
