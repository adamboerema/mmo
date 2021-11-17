using System;
using Common.Definitions;
using Common.Packets.ClientToServer.Auth;
using Common.Packets.ClientToServer.Movement;
using Server.Network.Handler;

namespace Server.Network.Router
{
    public class HandlerRouter: IHandlerRouter
    {
        private AuthHandler _authHandler;
        private PlayerHandler _playerHandler;

        public HandlerRouter(
            AuthHandler authHandler,
            PlayerHandler movementHandler)
        {
            _authHandler = authHandler;
            _playerHandler = movementHandler;
        }

        public void Route(string connectionId, IPacket handlerPacket)
        {
            switch(handlerPacket)
            {
                case LoginRequestPacket packet:
                    _authHandler.Handle(connectionId, packet);
                    break;
                case PlayerMovementPacket packet:
                    _playerHandler.Handle(connectionId, packet);
                    break;
            }
        }
    }
}
