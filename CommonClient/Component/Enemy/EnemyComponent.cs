using System;
using System.Numerics;
using Common.Component;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;
using Common.Utility;

namespace CommonClient.Component.Enemy
{
    public class EnemyComponent: IComponent
    {
        public string Id { get; init; }
        public EnemyType EnemyType { get; init; }

        private MovementModel _movement;
        private PathingModel _pathing;
        private CharacterModel _character;


        public EnemyComponent(EnemyConfiguration enemyConfiguration)
        {
            Id = enemyConfiguration.Id;
            EnemyType = enemyConfiguration.Type;
            _movement = enemyConfiguration.Movement;
            _pathing = enemyConfiguration.Pathing;
            _character = enemyConfiguration.Character;
        }

        public void Update(GameTick gameTick, World world)
        {
            // Update here
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
