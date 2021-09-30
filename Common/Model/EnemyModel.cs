﻿using System;
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

        private MovementModel _movement;

        private CharacterModel _character;

        public EnemyModel(
            string id,
            EnemyType type,
            SpawnModel spawnModel,
            MovementModel movementModel,
            CharacterModel characterModel)
        {
            Id = id;
            Type = type;
            _spawn = spawnModel;
            _movement = movementModel;
            _character = characterModel;
        }

        /// <summary>
        /// Character
        /// </summary>
        public Vector3 Coordinates => _character.Coordinates;
        public float MovementSpeed => _character.MovementSpeed;
        public void StopMove() => _character.StopMove();

        /// <summary>
        /// Movement
        /// </summary>
        public string EngageTargetId => _movement.EngageTargetId;
        public Vector3 MovementDestination => _movement.MovementDestination;
        public Vector3 GetRandomMovementPoint() => _movement.GetRandomMovementPoint();
        public bool ShouldEngage(Vector3 target) => _movement.ShouldEngage(_character.Coordinates, target);
        public bool ShouldDisengage(Vector3 target) => _movement.ShouldDisengage(_character.Coordinates, target);

        /// <summary>
        /// Spawn
        /// </summary>
        public bool ShouldRespawn(double timestamp) => _spawn.ShouldRespawn(timestamp);
        public Vector3 GetRandomSpawnPoint() => _spawn.GetRandomSpawnPoint();


        /// <summary>
        /// Engage target character
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public void EngageCharacter(string id, Vector3 position)
        {
            _movement.EngageTargetId = id;
            PathToPoint(
                _character.Coordinates,
                position,
                _character.MovementSpeed);
        }

        /// <summary>
        /// Disengage the target
        /// </summary>
        /// <returns></returns>
        public void DisengagePlayer()
        {
            _movement.EngageTargetId = null;
            _movement.LastDisengageTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _movement.MovementDestination = _character.Coordinates;
        }

        /// <summary>
        /// Update the destination
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        public void SetDestination(Vector3 destination)
        {
            _movement.MovementDestination = destination;
        }

        /// <summary>
        /// Increments the movement towards the movement destination
        /// </summary>
        /// <param name="elapsedTime"></param>
        public void MoveToDestination(double elapsedTime)
        {
            _character.MoveToPoint(_movement.MovementDestination, elapsedTime);
        }

        /// <summary>
        /// Should enemy start moving
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public bool ShouldStartMove(double timestamp)
        {
            var moveTime = _movement.LastMovementTime + _movement.MovementWaitSeconds;
            return moveTime < timestamp && _movement.EngageTargetId == null;
        }

        /// <summary>
        /// Should enemy stop movement
        /// </summary>
        /// <returns></returns>
        public bool ShouldStopMove()
        {
            return _character.IsMoving
                && _character.Coordinates == _movement.MovementDestination;
        }

        /// <summary>
        /// Path to destintation
        /// </summary>
        /// <param name="toPoint"></param>
        /// <returns></returns>
        public void PathToPoint(Vector3 toPoint)
        {
            PathToPoint(
                _character.Coordinates,
                toPoint,
                _character.MovementSpeed);
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
            _movement.LastMovementTime = DateTimeOffset.Now.ToUnixTimeSeconds();
            _character.Coordinates = fromPoint;
            _movement.MovementDestination = toPoint;
            _character.MovementSpeed = movementSpeed;
            _character.Direction = MovementUtility.GetDirectionToPoint(_character.Coordinates, toPoint);
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
            _character.Coordinates = respawnCoordinates;
            _movement.MovementDestination = respawnCoordinates;
            _character.IsMoving = false;
        }
    }
}
