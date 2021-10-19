using System;
using System.Numerics;
using Common.Base;
using Common.Model.Character;
using Common.Packets.ServerToClient.Player;
using Server.Bus.Packet;

namespace Server.Engine.Player
{
    public class PlayerComponent : IPlayerComponent
    {
        private IDispatchPacketBus _dispatchBus;
        private IPlayerStore _playerStore;

        public PlayerComponent(
            IDispatchPacketBus dispatchBus,
            IPlayerStore playersStore)
        {
            _dispatchBus = dispatchBus;
            _playerStore = playersStore;
        }

        public void Update(double elapsedTime, double timestamp)
        {
            // TODO: Update checks
        }

        public void InitializePlayer(string connectionId)
        {
            var player = CreateNewPlayer(connectionId);
            AddPlayer(player);
        }

        public void AddPlayer(PlayerModel player)
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
        private void DispatchOtherPlayers(PlayerModel player)
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
        private void DispatchConnectPlayer(PlayerModel player)
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
        /// Build the Player connect packet based on if it should be distributed
        /// as the client connecting or the global packet
        /// </summary>
        /// <param name="player">Player model</param>
        /// <param name="isClient">Is Player for the connecting client?</param>
        /// <returns></returns>
        private PlayerConnectPacket CreatePlayerConnectPacket(PlayerModel player, bool isClient) =>
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
        private PlayerModel CreateNewPlayer(string connectionId) =>
            new PlayerModel(
                id: connectionId,
                characterModel: new CharacterModel
                {
                    Name = "Test",
                },
                movementModel: new MovementModel {
                    Direction = Direction.DOWN,
                    Coordinates = new Vector3(0, 0, 0),
                    IsMoving = false,
                    MovementSpeed = 0.2f
                });

    }
}
