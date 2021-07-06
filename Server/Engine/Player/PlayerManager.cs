using System;
using System.Numerics;
using Common.Model;
using Common.Packets.ServerToClient.Movement;
using Common.Packets.ServerToClient.Player;
using Server.Bus.Connection;
using Server.Bus.Packet;

namespace Server.Engine.Player
{
    public class PlayerManager : IPlayerManager
    {
        private IDispatchPacketBus _dispatchBus;
        private IPlayerStore _playerStore;

        public PlayerManager(
            IDispatchPacketBus dispatchBus,
            IPlayerStore playersStore)
        {
            _dispatchBus = dispatchBus;
            _playerStore = playersStore;
        }

        public void AddPlayer(PlayerModel player)
        {
            _playerStore.Add(player);
            DispatchConnectPlayer(player);
            DispatchAllStartMovement(player);
            DispatchPlayerStartMovement(player);
        }

        public void RemovePlayer(string connectionId)
        {
            var player = _playerStore.Get(connectionId);
            if(player != null)
            {
                _playerStore.Remove(connectionId);
                DispatchDisconnectPlayer(player);
            }
        }

        public void CreatePlayer(string connectionId)
        {
            var player = CreateNewPlayer(connectionId);
            AddPlayer(player);
        }

        /// <summary>
        /// Dispatch all current player location to new player
        /// </summary>
        /// <param name="player">New Player</param>
        private void DispatchAllStartMovement(PlayerModel player)
        {
            var allPlayers = _playerStore.GetAll();
            foreach(var playerValue in allPlayers)
            {
                var connectedPlayer = playerValue.Value;
                var packet = new MovementOutputPacket
                {
                    PlayerId = connectedPlayer.Id,
                    X = connectedPlayer.Character.Coordinates.X,
                    Y = connectedPlayer.Character.Coordinates.Y,
                    Z = connectedPlayer.Character.Coordinates.Z,
                    MovementType = connectedPlayer.Character.MovementType
                };
                _dispatchBus.Publish(player.Id, packet);
            }
        }

        /// <summary>
        /// Dispatch player start movement to all players
        /// </summary>
        /// <param name="player">New Player</param>
        private void DispatchPlayerStartMovement(PlayerModel player)
        {
            var packet = new MovementOutputPacket
            {
                PlayerId = player.Id,
                X = player.Character.Coordinates.X,
                Y = player.Character.Coordinates.Y,
                Z = player.Character.Coordinates.Z,
                MovementType = player.Character.MovementType
            };
            _dispatchBus.Publish(packet);
        }

        /// <summary>
        /// Dispatch to all that player connected.
        /// </summary>
        /// <param name="player"></param>
        private void DispatchConnectPlayer(PlayerModel player)
        {
            var packet = new PlayerConnectPacket
            {
                PlayerId = player.Id
            };
            _dispatchBus.Publish(packet);
        }

        /// <summary>
        /// Dispatch to player disconnected
        /// </summary>
        /// <param name="player"></param>
        private void DispatchDisconnectPlayer(PlayerModel player)
        {
            var packet = new PlayerDisconnectPacket
            {
                PlayerId = player.Id
            };
            _dispatchBus.Publish(packet);
        }

        /// <summary>
        /// Get new player object
        /// </summary>
        /// <param name="connectionId">Connection id that connected</param>
        /// <returns></returns>
        private PlayerModel CreateNewPlayer(string connectionId) => new PlayerModel
        {
            // TODO: Replace with persistent data
            Id = connectionId,
            Character = new CharacterModel
            {
                Name = "Test",
                MovementType = MovementType.STOPPED,
                Coordinates = new Vector3(0, 0, 0)
            }
        };
    }
}
