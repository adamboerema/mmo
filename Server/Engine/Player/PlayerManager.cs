using System;
using Server.Network.Connection;

namespace Server.Engine.Player
{
    public class PlayerManager
    {
        private IConnectionDispatch _connectionDispatch;

        public PlayerManager(IConnectionDispatch connectionDispatch)
        {
            _connectionDispatch = connectionDispatch;
        }
    }
}
