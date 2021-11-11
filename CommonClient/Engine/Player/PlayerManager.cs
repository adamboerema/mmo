using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Packets.ClientToServer.Movement;
using Common.Store;
using CommonClient.Bus.Packet;
using CommonClient.Component.Player;

namespace CommonClient.Engine.Player
{
    public class PlayerManager: IPlayerManager
    {
        private IStore<string, PlayerComponent> _playerStore;
        private IDispatchPacketBus _dispatchPacketBus;

        public PlayerManager(
            IStore<string, PlayerComponent> playerStore,
            IDispatchPacketBus dispatchPacketBus)
        {
            _playerStore = playerStore;
            _dispatchPacketBus = dispatchPacketBus;
        }

        public void Update(GameTick gameTime)
        {
            throw new NotImplementedException();
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

        public void UpdateClientMovementInput(Direction direction, bool isMoving)
        {
            _playerStore.UpdateClientMovement(direction, isMoving);

            _dispatchPacketBus.Publish(new PlayerMovementPacket
            {
                Direction = direction,
                IsMoving = isMoving
            });
        }

        public void UpdatePlayerCoordinatesOutput(
            string playerId,
            Vector3 coordinates,
            Direction movementType,
            bool isMoving)
        {
            _playerStore.UpdateMovement(playerId, coordinates, movementType, isMoving);
        }

        public void UpdatePlayer(ClientPlayerEntity model)
        {
            _playerStore.Update(model);
        }

        public void RemovePlayer(string playerId)
        {
            _playerStore.Remove(playerId);
        }

        public IEnumerable<ClientPlayerEntity> GetPlayers()
        {
            return _playerStore.GetAll().Values;
        }

        public ClientPlayerEntity GetClientPlayer()
        {
            return _playerStore.GetClientPlayer();
        }

        /// <summary>
        /// Create 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private ClientPlayerEntity CreateNewPlayer(
            string playerId,
            bool isClient,
            Vector3 position,
            Direction movementType)
        {
            return new ClientPlayerEntity(
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
