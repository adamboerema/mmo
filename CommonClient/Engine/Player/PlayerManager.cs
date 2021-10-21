using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Base;
using Common.Model;
using Common.Model.Character;
using Common.Model.Shared;

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
            Direction movementType)
        {
            var player = CreateNewPlayer(
                playerId,
                isClient,
                position,
                movementType);
            _playerStore.Add(player);
        }

        public void UpdatePlayer(ClientPlayerModel model)
        {
            _playerStore.Update(model);
        }

        public void RemovePlayer(string playerId)
        {
            _playerStore.Remove(playerId);
        }

        public IEnumerable<ClientPlayerModel> GetPlayers()
        {
            return _playerStore.GetAll().Values;
        }

        public ClientPlayerModel GetClientPlayer()
        {
            return _playerStore.GetClientPlayer();
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
            Direction movementType)
        {
            return new ClientPlayerModel(
                id: playerId,
                isClient: isClient,
                characterModel: new CharacterModel
                {
                    Name = "test"
                },
                movementModel: new MovementModel
                {
                    Direction = movementType,
                    Coordinates = position,
                    IsMoving = false,
                    MovementSpeed = 0.2f
                });
        }
    }
}
