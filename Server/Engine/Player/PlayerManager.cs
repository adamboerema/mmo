using System;
using System.Collections.Generic;
using Common.Bus;
using Common.Network.Packet.Definitions.Schema.Movement;
using Common.Network.Packet.Definitions.Schema.Player;
using Server.Bus.Connection;
using Server.Bus.Packet;

namespace Server.Engine.Player
{
    public class PlayerManager : IPlayerManager, IEventBusListener<ConnectionEvent>
    {
        private Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private IConnectionBus _connectionBus;
        private IDispatchPacketBus _dispatchBus;

        public PlayerManager(
            IConnectionBus connectionBus,
            IDispatchPacketBus dispatchBus)
        {
            _dispatchBus = dispatchBus;
            _connectionBus = connectionBus;
            _connectionBus.Subscribe(this);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player.Id, player);
            DispatchConnectPlayer(player);
            DispatchAllStartMovement(player);
            DispatchPlayerStartMovement(player);
        }

        public void RemovePlayer(string connectionId)
        {
            if(_players.ContainsKey(connectionId))
            {
                var player = _players[connectionId];
                _players.Remove(connectionId);
                DispatchDisconnectPlayer(player);
            }
        }

        public void Handle(ConnectionEvent eventObject)
        {
            switch (eventObject.State)
            {
                case ConnectionState.CONNECT:
                    var player = CreateNewPlayer(eventObject.Id);
                    AddPlayer(player);
                    break;
                case ConnectionState.DISCONNECT:
                    RemovePlayer(eventObject.Id);
                    break;
            }
        }

        /// <summary>
        /// Dispatch all current player location to new player
        /// </summary>
        /// <param name="player">New Player</param>
        private void DispatchAllStartMovement(Player player)
        {
            foreach(var playerValue in _players)
            {
                var connectedPlayer = playerValue.Value;
                var packet = new MovementOutputPacket
                {
                    PlayerId = connectedPlayer.Id,
                    X = connectedPlayer.Character.X,
                    Y = connectedPlayer.Character.Y,
                    Z = connectedPlayer.Character.Z,
                    MovementType = connectedPlayer.Character.MovementType
                };
                _dispatchBus.Publish(player.Id, packet);
            }
        }

        /// <summary>
        /// Dispatch player start movement to all players
        /// </summary>
        /// <param name="player">New Player</param>
        private void DispatchPlayerStartMovement(Player player)
        {
            var packet = new MovementOutputPacket
            {
                PlayerId = player.Id,
                X = player.Character.X,
                Y = player.Character.Y,
                Z = player.Character.Z,
                MovementType = player.Character.MovementType
            };
            _dispatchBus.Publish(packet);
        }

        /// <summary>
        /// Dispatch to all that player connected.
        /// </summary>
        /// <param name="player"></param>
        private void DispatchConnectPlayer(Player player)
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
        private void DispatchDisconnectPlayer(Player player)
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
        private Player CreateNewPlayer(string connectionId) => new Player
        {
            // TODO: Replace with persistent data
            Id = connectionId,
            Character = new Character
            {
                Name = "Test",
                MovementType = MovementType.STOPPED,
                X = 0,
                Y = 0,
                Z = 0
            }
        };
    }
}
