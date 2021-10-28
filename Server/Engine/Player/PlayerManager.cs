using System;
using System.Numerics;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Packets.ServerToClient.Player;
using Common.Utility;
using Server.Bus.Packet;
using Server.Component.Player;
using Server.Network.Dispatch;

namespace Server.Engine.Player
{
    public class PlayerManager : IPlayerManager
    {
        private IDispatchPacketBus _dispatchBus;
        private IPlayerStore _playerStore;
        private IPlayerDispatch _playerDispatch;

        public PlayerManager(
            IDispatchPacketBus dispatchBus,
            IPlayerDispatch playerDispatch,
            IPlayerStore playersStore)
        {
            _dispatchBus = dispatchBus;
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
            DispatchConnectPlayer(player);
            DispatchOtherPlayers(player);

            // Store after dispatch to avoid duplicates
            _playerStore.Add(player);
        }

        public void RemovePlayer(string connectionId)
        {
            _playerStore.Remove(connectionId);
            DispatchDisconnectPlayer(connectionId);
        }

        /// <summary>
        /// Dispatch all current player location to new player
        /// </summary>
        /// <param name="player">New Player to notify</param>
        private void DispatchOtherPlayers(PlayerComponent player)
        {
            var allPlayers = _playerStore.GetAll();
            foreach(var playerValue in allPlayers)
            {
                var otherPlayer = playerValue.Value;
                Console.WriteLine($"OTHER PLAYER: {otherPlayer.Id}");
                var packet = CreatePlayerConnectPacket(otherPlayer, false);
                _dispatchBus.Publish(player.Id, packet);
            }
        }

        /// <summary>
        /// Dispatch to all that player connected.
        /// </summary>
        /// <param name="player">Player model</param>
        /// <param name="isClient">Same client that requested connection</param>
        private void DispatchConnectPlayer(PlayerComponent player)
        {
            _dispatchBus.PublishExcept(player.Id, CreatePlayerConnectPacket(player, false));
            _dispatchBus.Publish(player.Id, CreatePlayerConnectPacket(player, true));
        }

        /// <summary>
        /// Dispatch to player disconnected
        /// </summary>
        /// <param name="connectionId"></param>
        private void DispatchDisconnectPlayer(string connectionId)
        {
            var packet = new PlayerDisconnectPacket
            {
                PlayerId = connectionId
            };
            _dispatchBus.Publish(packet);
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
                DispatchMovementUpdate(player);
            }
        }

        /// <summary>
        /// Build the Player connect packet based on if it should be distributed
        /// as the client connecting or the global packet
        /// </summary>
        /// <param name="player">Player model</param>
        /// <param name="isClient">Is Player for the connecting client?</param>
        /// <returns></returns>
        private PlayerConnectPacket CreatePlayerConnectPacket(PlayerComponent player, bool isClient) =>
            new PlayerConnectPacket
            {
                PlayerId = player.Id,
                IsClient = isClient,
                IsMoving = player.IsMoving,
                Position = new Vector3(
                        player.Coordinates.X,
                        player.Coordinates.Y,
                        player.Coordinates.Z),
                MovementType = player.Direction
            };

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
