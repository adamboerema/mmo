using System;
using Common.Definitions;
using Common.Packets.ClientToServer.Auth;
using Common.Packets.ClientToServer.Movement;

namespace Server.Network.Handler.Factory
{
    public class HandlerRouter: IHandlerRouter
    {
        private AuthHandler _authHandler;
        private MovementHandler _movementHandler;

        public HandlerRouter(
            AuthHandler authHandler,
            MovementHandler movementHandler)
        {
            _authHandler = authHandler;
            _movementHandler = movementHandler;
        }

        public void Route(string connectionId, IPacket handlerPacket)
        {
            switch(handlerPacket)
            {
                case LoginRequestPacket packet:
                    _authHandler.Handle(connectionId, packet);
                    break;
                case MovementInputPacket packet:
                    _movementHandler.Handle(connectionId, packet);
                    break;
            }
        }
    }
}
