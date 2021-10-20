using System;
using System.Numerics;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Utility;

namespace Common.Base
{
    public class EnemyModel
    {
        public string Id { get; }

        public EnemyType Type { get; }

        private SpawnModel _spawn;

        private PathingModel _pathing;

        private MovementModel _movement;

        private CharacterModel _character;

        private CombatModel _combat;

        private CollisionModel _collision;

        public EnemyModel(
            string id,
            EnemyType type,
            SpawnModel spawnModel,
            PathingModel pathingModel,
            CharacterModel characterModel,
            MovementModel movementModel,
            CombatModel combatModel,
            CollisionModel collisionModel)
        {
            Id = id;
            Type = type;
            _spawn = spawnModel;
            _pathing = pathingModel;
            _character = characterModel;
            _movement = movementModel;
            _combat = combatModel;
            _collision = collisionModel;
        }

        /// <summary>
        /// Movement
        /// </summary>
        public Vector3 Coordinates => _movement.Coordinates;
        public float MovementSpeed => _movement.MovementSpeed;
        public void StopMove() => _movement.StopMove();

        /// <summary>
        /// Movement
        /// </summary>
        public string EngageTargetId => _pathing.EngageTargetId;
        public Vector3 MovementDestination => _pathing.MovementDestination;
        public Vector3 GetRandomMovementPoint() => _pathing.GetRandomMovementPoint();
        public bool ShouldEngage(Vector3 target) => _pathing.ShouldEngage(_movement.Coordinates, target);
        public bool ShouldDisengage(Vector3 target) => _pathing.ShouldDisengage(_movement.Coordinates, target);

        /// <summary>
        /// Spawn
        /// </summary>
        public bool ShouldRespawn(double timestamp) => _spawn.ShouldRespawn(timestamp);
        public Vector3 GetRandomSpawnPoint() => _spawn.GetRandomSpawnPoint();

        /// <summary>
        /// Combat
        /// </summary>
        public float GetAttackDistance() => _combat.GetAttackDistance(_collision.GetCollisionDistance());

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void EngageCharacter(string id, Vector3 position)
        {
            _pathing.EngageTargetId = id;
            PathToPoint(
                _movement.Coordinates,
                position,
                _movement.MovementSpeed);
        }

        /// <summary>
        /// Disengage the target
        /// </summary>
        /// <returns></returns>
        public void DisengageCharacter()
        {
            _pathing.EngageTargetId = null;
            _pathing.LastDisengageTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _pathing.MovementDestination = _movement.Coordinates;
        }

        /// <summary>
        /// Attack target
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void AttackTarget(double elapsedTime)
        {

        }


        /// <summary>
        /// Update the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public void SetEngageDestination(Vector3 destination)
        {
            _pathing.MovementDestination = destination;
        }

        /// <summary>
        /// Increments the movement towards the movement destination
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void MoveToDestination(double elapsedTime)
        {
            var distance = MovementUtility.GetAbsoluteDistanceToPoint(
                _movement.Coordinates,
                _pathing.MovementDestination);

            var targetDistance = _pathing.EngageTargetId == null
                ? _collision.GetCollisionDistance()
                : GetAttackDistance();

            if (distance > targetDistance)
            {
                _movement.MoveToPoint(_pathing.MovementDestination, elapsedTime);
            }            
        }

        /// <summary>
        /// Should enemy start moving
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public bool ShouldStartMove(double timestamp)
        {
            return _pathing.ShouldStartMove(timestamp)
                && _pathing.EngageTargetId == null;
        }

        /// <summary>
        /// Should enemy stop movement only when enemy reaches target.
        /// For engaged enemies they should never stop
        /// </summary>
        /// <returns></returns>
        public bool ShouldStopMove()
        {
            return _movement.IsMoving
                && _movement.Coordinates == _pathing.MovementDestination;
        }

        /// <summary>
        /// Path to destintation
        /// </summary>
        /// <param name="toPoint"></param>
        /// <returns></returns>
        public void PathToPoint(Vector3 toPoint)
        {
            PathToPoint(
                _movement.Coordinates,
                toPoint,
                _movement.MovementSpeed);
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
        /// Respawn the enemy
        /// </summary>
        /// <param name="respawnCoordinates"></param>
        /// <returns></returns>
        public void Respawn()
        {
            var respawnCoordinates = _spawn.GetRandomSpawnPoint();
            _spawn.SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _spawn.IsAlive = true;
            _movement.Coordinates = respawnCoordinates;
            _pathing.MovementDestination = respawnCoordinates;
            _movement.IsMoving = false;
        }
    }
}
