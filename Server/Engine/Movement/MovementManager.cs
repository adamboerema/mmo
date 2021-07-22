using System;
using System.Numerics;
using Common.Bus;
using Common.Model;
using Common.Extensions;
using Common.Packets.ServerToClient.Movement;
using Server.Bus.Game;
using Server.Bus.Packet;
using Server.Engine.Player;

namespace Server.Engine.Movement
{
    public class MovementManager: IMovementManager, IEventBusListener<GameLoopEvent>
    {
        private const float PLAYER_SPEED = 0.05f;
        private const int MAX_WIDTH = 10000;
        private const int MAX_HEIGHT = 10000;

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
            var speed = PLAYER_SPEED * (float) elapsedTime;
            foreach (var playerValue in players)
            {
                playerValue.Value.MoveCoordinates(speed, MAX_WIDTH, MAX_HEIGHT);
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
                Position = new Vector3(
                    player.Character.Coordinates.X,
                    player.Character.Coordinates.Y,
                    player.Character.Coordinates.Z),
                MovementType = player.Character.MovementType
            });
        }
    }
}
