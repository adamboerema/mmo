using System;
using System.Numerics;
using Common.Bus;
using Common.Model;
using Common.Packets.ServerToClient.Movement;
using Server.Bus.Game;
using Server.Bus.Packet;
using Server.Engine.Player;

namespace Server.Engine.Movement
{
    public class MovementManager: IMovementManager, IEventBusListener<GameLoopEvent>
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;
        private readonly IPlayerStore _playerStore;
        private readonly IGameLoopBus _gameLoopBus;

        public MovementManager(
            IDispatchPacketBus dispatchPacketBus,
            IGameLoopBus gameLoopBus,
            IPlayerStore playerStore)
        {
            _dispatchPacketBus = dispatchPacketBus;
            _playerStore = playerStore;

            _gameLoopBus = gameLoopBus;
            _gameLoopBus.Subscribe(this);
        }

        public void Handle(GameLoopEvent eventObject)
        {
            Update(eventObject.ElapsedTime);
        }

        public void Update(double elapsedTime)
        {
            UpdateCoordinatesOfPlayers(elapsedTime);
        }

        public void UpdateMovementInput(string playerId, MovementType movementType)
        {
            var player = _playerStore.Get(playerId);
            if (player != null)
            {
                player.Character.MovementType = movementType;
                _playerStore.Update(player);
                DispatchMovementUpdate(player);
            }
        }

        /// <summary>
        /// Updates the coordinates of the player
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void UpdateCoordinatesOfPlayers(double elapsedTime)
        {
            var players = _playerStore.GetAll();
            var speed = 0.1f * (float) elapsedTime;
            foreach (var playerValue in players)
            {
                var character = playerValue.Value.Character;
                var coordinates = character.Coordinates;
                switch(character.MovementType)
                {
                    case MovementType.UP:
                        coordinates.Y += coordinates.Y * speed;
                        break;
                    case MovementType.LEFT:
                        coordinates.X -= coordinates.X * speed;
                        break;
                    case MovementType.RIGHT:
                        coordinates.X += coordinates.X * speed;
                        break;
                    case MovementType.DOWN:
                        coordinates.Y -= coordinates.Y * speed;
                        break;
                    case MovementType.UP_LEFT:
                        coordinates.X -= coordinates.X * speed;
                        coordinates.Y += coordinates.Y * speed;
                        break;
                    case MovementType.UP_RIGHT:
                        coordinates.X += coordinates.X * speed;
                        coordinates.Y += coordinates.Y * speed;
                        break;
                    case MovementType.DOWN_LEFT:
                        coordinates.X -= coordinates.X * speed;
                        coordinates.Y -= coordinates.Y * speed;
                        break;
                    case MovementType.DOWN_RIGHT:
                        coordinates.X += coordinates.X * speed;
                        coordinates.Y -= coordinates.Y * speed;
                        break;
                    case MovementType.STOPPED:
                        break;
                }
                _playerStore.Update(playerValue.Value);
            }
        }

        /// <summary>
        /// Dispatch the movement update to all of the clients
        /// </summary>
        /// <param name="player"></param>
        private void DispatchMovementUpdate(PlayerModel player)
        {
            _dispatchPacketBus.Publish(new MovementOutputPacket
            {
                PlayerId = player.Id,
                X = player.Character.Coordinates.X,
                Y = player.Character.Coordinates.Y,
                Z = player.Character.Coordinates.Z,
                MovementType = player.Character.MovementType
            });
        }
    }
}
