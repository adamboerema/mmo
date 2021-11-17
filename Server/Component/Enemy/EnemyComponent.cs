using System;
using System.Numerics;
using Common.Component;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;
using Common.Store;
using Common.Utility;
using Server.Component.Player;
using Server.Network.Dispatch;

namespace Server.Component.Enemy
{
    public class EnemyComponent: IComponent
    {
        public string Id { get; init; }
        public readonly EnemyType Type;

        public Vector3 Coordinates => _movement.Coordinates;

        private SpawnModel _spawn;
        private PathingModel _pathing;
        private MovementModel _movement;
        private CharacterModel _character;
        private CombatModel _combat;
        private CollisionModel _collision;

        private readonly IEnemyDispatch _enemyDispatch;
        private readonly ComponentStore<PlayerComponent> _playerStore;

        public EnemyComponent(
            EnemyConfiguration config,
            IEnemyDispatch enemyDispatch,
            ComponentStore<PlayerComponent> playerStore)
        {
            Id = config.Id;
            Type = config.Type;
            _spawn = config.Spawn;
            _pathing = config.Pathing;
            _movement = config.Movement;
            _character = config.Character;
            _combat = config.Combat;
            _collision = config.Collision;

            _enemyDispatch = enemyDispatch;
            _playerStore = playerStore;

            Initialize();
        }

        /// <summary>
        /// Initialize
        /// </summary>
        private void Initialize()
        {
            RespawnEnemy();
        }

        /// <summary>
        /// Game Tick
        /// </summary>
        /// <param name="gameTick"></param>
        public void Update(GameTick gameTick, World world)
        {
            SpawnEnemy(gameTick);
            EngagePlayer(gameTick);
            StartMovement(gameTick);
            MoveEnemy(gameTick);
            StopMovement(gameTick);
        }

        /// <summary>
        /// Dispatch enemy to player id
        /// </summary>
        /// <param name="playerId"></param>
        public void DispatchEnemyToPlayer(string playerId)
        {
            _enemyDispatch.DispatchEnemyToPlayer(
                playerId,
                Id,
                Type,
                _movement.Coordinates,
                _movement.MovementSpeed,
                _pathing.EngageTargetId,
                _pathing.MovementDestination);
        }

        /// <summary>
        /// Trigger movement 
        /// </summary>
        /// <param name="elaspedTime"></param>
        /// <param name="timestamp"></param>
        /// <param name="enemy"></param>
        private void MoveEnemy(GameTick gameTick)
        {
            var distance = MovementUtility.GetAbsoluteDistanceToPoint(
                _movement.Coordinates,
                _pathing.MovementDestination);

            var targetDistance = _pathing.EngageTargetId == null
                ? _collision.GetCollisionDistance()
                : _combat.GetAttackDistance(_collision.GetCollisionDistance());

            if (distance > targetDistance)
            {
                _movement.MoveToPoint(_pathing.MovementDestination, gameTick.ElapsedTime);
            }
            else if (_pathing.EngageTargetId != null)
            {
                AttackTarget(gameTick);
            }
        }

        /// <summary>
        /// Find dead enemies and respawn if a certain time has past
        /// </summary>
        /// <param name="timestamp">Unix timestamp</param>
        private void SpawnEnemy(GameTick gameTick)
        {
            if (_spawn.ShouldRespawn(gameTick))
            {
                RespawnEnemy();
            }
        }

        /// <summary>
        /// Start Movement
        /// </summary>
        /// <param name="gameTick"></param>
        private void StartMovement(GameTick gameTick)
        {
            if (ShouldStartMove(gameTick))
            {
                var movementPoint = _pathing.GetRandomMovementPoint();
                PathToPoint(movementPoint);
                _enemyDispatch.DispatchEnemyMovement(
                    Id,
                    _movement.Coordinates,
                    _pathing.MovementDestination,
                    _movement.MovementSpeed);
            }
        }

        /// <summary>
        /// Stop movement
        /// </summary>
        /// <param name="gameTick"></param>
        private void StopMovement(GameTick gameTick)
        {
            if (ShouldStopMove())
            {
                _movement.StopMove();
                _enemyDispatch.DispatchEnemyMovement(
                    Id,
                    _movement.Coordinates,
                    _pathing.MovementDestination,
                    _movement.MovementSpeed);
            }
        }

        /// <summary>
        /// Engage player
        /// </summary>
        /// <param name="gameTick"></param>
        private void EngagePlayer(GameTick gameTick)
        {
            if (_pathing.EngageTargetId == null)
            {
                CheckEnemyEngage(gameTick);
            }
            else
            {
                CheckEnemyDisengage(gameTick);
            }
        }

        /// <summary>
        /// Attack target
        /// </summary>
        /// <param name="elapsedTime"></param>
        private void AttackTarget(GameTick gameTime)
        {
            // TODO: attack logic
        }

        /// <summary>
        /// Check if enemy should start engaging
        /// </summary>
        private void CheckEnemyEngage(GameTick gameTick)
        {
            foreach (var player in _playerStore.GetAll().Values)
            {
                if (_pathing.ShouldEngage(_movement.Coordinates, player.Coordinates))
                {
                    EngageTarget(player.Id, player.Coordinates);
                    _enemyDispatch.DispatchEnemyEngage(Id, player.Id);
                }
            }
        }

        /// <summary>
        /// Check if enemy should disengage
        /// </summary>
        /// <param name="enemy"></param>
        private void CheckEnemyDisengage(GameTick gameTick)
        {
            var player = _playerStore.Get(_pathing.EngageTargetId);
            if (player != null)
            {
                if (_pathing.ShouldDisengage(_movement.Coordinates, player.Coordinates))
                {
                    _pathing.DisengagetoPoint(_movement.Coordinates);
                    _enemyDispatch.DispatchEnemyDisenage(Id);
                }
                else
                {
                    _pathing.MovementDestination = player.Coordinates;
                }
            }
            else
            {
                _pathing.DisengagetoPoint(_movement.Coordinates);
                _enemyDispatch.DispatchEnemyDisenage(Id);
            }
        }

        /// <summary>
        /// Will reset timers and respawn enemy
        /// </summary>
        /// <param name="enemy"></param>
        private void RespawnEnemy()
        {
            var respawnCoordinates = _spawn.GetRandomSpawnPoint();
            _spawn.SpawnTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _spawn.IsAlive = true;
            _movement.Coordinates = respawnCoordinates;
            _pathing.MovementDestination = respawnCoordinates;
            _movement.IsMoving = false;

            _enemyDispatch.DispatchEnemySpawn(
                Id,
                Type,
                _movement.Coordinates);
        }

        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="targetId">Character Id</param>
        /// <returns></returns>
        private void EngageTarget(string targedId, Vector3 position)
        {
            _pathing.EngageTargetId = targedId;
            PathToPoint(_movement.Coordinates, position, _movement.MovementSpeed);
        }

        /// <summary>
        /// Should enemy start moving
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private bool ShouldStartMove(GameTick gameTime)
        {
            return _pathing.ShouldStartMove(gameTime.Timestamp)
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
        private void PathToPoint(Vector3 toPoint)
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
        private void PathToPoint(
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
    }
}
