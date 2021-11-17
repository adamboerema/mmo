using System;
using System.Numerics;
using Common.Component;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;
using Common.Store;
using Common.Utility;
using CommonClient.ComponentStore.Player;

namespace CommonClient.ComponentStore.Enemy
{
    public class EnemyComponent: IComponent
    {
        public string Id { get; init; }
        public EnemyType EnemyType { get; init; }
        public Vector3 Coordinates => _movement.Coordinates;

        private MovementModel _movement;
        private PathingModel _pathing;
        private CharacterModel _character;

        private ComponentStore<PlayerComponent> _playerStore;

        public EnemyComponent(
            EnemyConfiguration enemyConfiguration,
            ComponentStore<PlayerComponent> playerStore)
        {
            Id = enemyConfiguration.Id;
            EnemyType = enemyConfiguration.Type;
            _movement = enemyConfiguration.Movement;
            _pathing = enemyConfiguration.Pathing;
            _character = enemyConfiguration.Character;

            _playerStore = playerStore;
        }

        public void Update(GameTick gameTick, World world)
        {
            if (_pathing.EngageTargetId != null)
            {
                var player = _playerStore.Get(_pathing.EngageTargetId);
                if (player != null)
                {
                    PathToPoint(
                        _movement.Coordinates,
                        player.Coordinates,
                        _movement.MovementSpeed);
                }
            }
        }

        /// <summary>
        /// Starts movement towards point
        /// </summary>
        /// <param name="point"></param>
        /// <returns></returns>
        public void PathToPoint(
            Vector3 fromPoint,
            Vector3 toPoint,
            float movementSpeed)
        {
            _pathing.LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _movement.Coordinates = fromPoint;
            _pathing.MovementDestination = toPoint;
            _movement.MovementSpeed = movementSpeed;
            _movement.Direction = MovementUtility.GetDirectionToPoint(_movement.Coordinates, toPoint);
        }

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="targetId">Character Id</param>
        /// <returns></returns>
        public void EngageTarget(string targedId, Vector3 position)
        {
            _pathing.EngageTargetId = targedId;
            PathToPoint(_movement.Coordinates, position, _movement.MovementSpeed);
        }

        /// <summary>
        /// Disengage to point
        /// </summary>
        /// <param name="coordinates"></param>
        public void Disengage()
        {
            _pathing.DisengagetoPoint(_movement.Coordinates);
        }
    }
}
