using System;
using System.Collections.Generic;
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

        public void InitializePlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            MovementType movementType)
        {
            var player = CreateNewPlayer(
                playerId,
                isClient,
                position,
                movementType);
            _playerStore.Add(player);
        }

        public void RemovePlayer(string playerId)
        {
            _playerStore.Remove(playerId);
        }

        public ICollection<KeyValuePair<string, ClientPlayerModel>> GetPlayers()
        {
            return _playerStore.GetAll();
        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ClientPlayerModel CreateNewPlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            MovementType movementType)
        {
            return new ClientPlayerModel
            {
                Id = playerId,
                IsClient = isClient,
                Character = new CharacterModel
                {
                    Name = "test",
                    Coordinates = position,
                    MovementType = movementType
                }
            };
        }
    }
}
