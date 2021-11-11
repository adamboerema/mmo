using System;
using System.Numerics;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Store;
using Common.Utility;
using Server.ComponentStore.Player;
using Server.Network.Dispatch;

namespace Server.Engine.Player
{
    public class PlayerManager : IPlayerManager
    {
        private IStore<string, PlayerComponent> _playerStore;
        private IPlayerDispatch _playerDispatch;

        public PlayerManager(
            IPlayerDispatch playerDispatch,
            IStore<string, PlayerComponent> playersStore)
        {
            _playerDispatch = playerDispatch;
            _playerStore = playersStore;
        }

        public void Update(GameTick gameTick)
        {
            var players = _playerStore.GetAll();
            foreach (var player in players.Values)
            {
                player.Update(gameTick, WorldUtility.GetWorld());
                _playerStore.Update(player);
            }
        }

        public void InitializePlayer(string connectionId)
        {
            var player = CreateNewPlayer(connectionId);
            AddPlayer(player);
        }

        public void AddPlayer(PlayerComponent player)
        {
            player.DispatchAsClient();
            DispatchOtherPlayers(player.Id);

            // Store after dispatch to avoid duplicates
            _playerStore.Add(player);
        }

        public void RemovePlayer(string connectionId)
        {
            var player = _playerStore.Get(connectionId);
            player?.DispatchDisconnectPlayer();
            _playerStore.Remove(connectionId);
        }

        /// <summary>
        /// Dispatch all current player location to new player
        /// </summary>
        /// <param name="player">New Player to notify</param>
        private void DispatchOtherPlayers(string connectionId)
        {
            foreach(var otherPlayer in _playerStore.GetAll().Values)
            {
                otherPlayer.DispatchToOtherPlayer(connectionId);
            }
        }

        /// <summary>
        /// Update the movement input of the player
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="movementType"></param>
        /// <param name="isMoving"></param>
        public void UpdateMovementInput(string playerId, Direction movementType, bool isMoving)
        {
            var player = _playerStore.Get(playerId);
            if (player != null)
            {
                player.UpdateDirection(movementType, isMoving);
                _playerStore.Update(player);
            }
        }

        /// <summary>
        /// Get new player object
        /// </summary>
        /// <param name="connectionId">Connection id that connected</param>
        /// <returns></returns>
        private PlayerComponent CreateNewPlayer(string connectionId) =>
            new PlayerComponent(
                new PlayerConfiguration
                {
                    Id = connectionId,
                    Character = new CharacterModel
                    {
                        Name = "Test"
                    },
                    Movement = new MovementModel
                    {
                        Direction = Direction.DOWN,
                        Coordinates = new Vector3(0, 0, 0),
                        IsMoving = false,
                        MovementSpeed = 0.2f
                    }
                },
                _playerDispatch);
    }
}
