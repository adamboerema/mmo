using System;
using System.Numerics;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Store;
using Common.Utility;
using CommonClient.ComponentStore.Player;
using CommonClient.Network.Dispatch;

namespace CommonClient.Engine.Player
{
    public class PlayerManager: IPlayerManager
    {
        private PlayerComponent _clientPlayer;
        private ComponentStore<PlayerComponent> _playerStore;
        private IPlayerDispatch _playerDispatch;

        public PlayerManager(
            ComponentStore<PlayerComponent> playerStore,
            IPlayerDispatch playerDispatch)
        {
            _playerStore = playerStore;
            _playerDispatch = playerDispatch;
        }

        public void Update(GameTick gameTime)
        {
            var world = WorldUtility.GetWorld();
            foreach (var player in _playerStore.GetAll().Values)
            {
                player.Update(gameTime, world);
                _playerStore.Update(player);
            }
        }

        public PlayerComponent GetClientPlayer()
        {
            return _clientPlayer;
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

            if (isClient)
            {
                _clientPlayer = player;
            }
        }

        public void UpdateClientMovementInput(Direction direction, bool isMoving)
        {
            if(_clientPlayer != null)
            {
                _clientPlayer.UpdateDirection(direction, isMoving);
            }
        }

        public void UpdatePlayerCoordinatesOutput(
            string playerId,
            Vector3 coordinates,
            Direction movementType,
            bool isMoving)
        {
            var player = _playerStore.Get(playerId);
            player.UpdateCoordinates(coordinates, movementType, isMoving);
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
        private PlayerComponent CreateNewPlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            Direction movementType)
        {
            return new PlayerComponent(
                new PlayerConfiguration
                {
                    Id = playerId,
                    IsClient = isClient,
                    Character = new CharacterModel
                    {
                        Name = "test"
                    },
                    Movement = new MovementModel
                    {
                        Direction = movementType,
                        Coordinates = position,
                        IsMoving = false,
                        MovementSpeed = 0.2f
                    },
                    Collision = new CollisionModel
                    {
                        Bounds = new Bounds(10, 10)
                    }
                },
                _playerDispatch);
        }
    }
}
