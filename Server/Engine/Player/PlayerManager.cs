using System;
using System.Collections.Generic;
using Common.Bus;
using Server.Bus.Connection;

namespace Server.Engine.Player
{
    public class PlayerManager : IPlayerManager, IEventBusListener<ConnectionEvent>
    {
        private Dictionary<string, Player> _players = new Dictionary<string, Player>();
        private IConnectionBus _connectionBus;

        public PlayerManager(IConnectionBus connectionBus)
        {
            _connectionBus = connectionBus;
            _connectionBus.Subscribe(this);
        }

        public void AddPlayer(Player player)
        {
            _players.Add(player.ConnectionId, player);
        }

        public void RemovePlayer(string connectionId)
        {
            _players.Remove(connectionId);
        }

        public void Handle(ConnectionEvent eventObject)
        {
            switch (eventObject.State)
            {
                case ConnectionState.CONNECT:
                    var player = GetNewPlayer(eventObject.Id);
                    AddPlayer(player);
                    break;
                case ConnectionState.DISCONNECT:
                    RemovePlayer(eventObject.Id);
                    break;
            }
        }

        /// <summary>
        /// Get new player object
        /// </summary>
        /// <param name="connectionId">Connection id that connected</param>
        /// <returns></returns>
        private Player GetNewPlayer(string connectionId) => new Player
        {
            ConnectionId = connectionId
        };
    }
}
