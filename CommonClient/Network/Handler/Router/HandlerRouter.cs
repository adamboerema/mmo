using System;
using Common.Definitions;
using Common.Packets.ServerToClient.Enemy;
using Common.Packets.ServerToClient.Movement;
using Common.Packets.ServerToClient.Player;

namespace CommonClient.Network.Handler.Router
{
    public class HandlerRouter: IHandlerRouter
    {
        private PlayerHandler _playerHandler;
        private MovementHandler _movementHandler;
        private EnemyHandler _enemyHandler;

        public HandlerRouter(
            PlayerHandler playerHandler,
            MovementHandler movementHandler,
            EnemyHandler enemyHandler)
        {
            _playerHandler = playerHandler;
            _movementHandler = movementHandler;
            _enemyHandler = enemyHandler;
        }

        public void Route(IPacket handlerPacket)
        {
            switch (handlerPacket)
            {
                // Movement
                case MovementOutputPacket packet:
                    _movementHandler.Handle(packet);
                    break;

                // Player
                case PlayerConnectPacket packet:
                    _playerHandler.Handle(packet);
                    break;
                case PlayerDisconnectPacket packet:
                    _playerHandler.Handle(packet);
                    break;

                // Enemy
                case EnemySpawnPacket packet:
                    _enemyHandler.Handle(packet);
                    break;
                case EnemyMovementPacket packet:
                    _enemyHandler.Handle(packet);
                    break;
            }
        }

    }
}
