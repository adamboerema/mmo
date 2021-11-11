using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Entity;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Store;
using CommonClient.Component.Enemy;
using CommonClient.Component.Player;

namespace CommonClient.Engine.Enemy
{
    public class EnemyManager: IEnemyManager
    {
        private readonly IStore<string, EnemyComponent> _enemyStore;
        private readonly IStore<string, PlayerComponent> _playerStore;

        public EnemyManager(
            IStore<string, EnemyComponent> enemyStore,
            IStore<string, PlayerComponent> playerStore)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
        }

        public void Update(GameTick gameTime)
        {
            throw new NotImplementedException();
        }

        public void SpawnEnemy(
            string enemyId,
            EnemyType enemyType,
            string targetId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed)
        {
            var enemy = CreateEnemy(
                enemyId,
                enemyType,
                targetId,
                position,
                movementDestination,
                movementSpeed);
            _enemyStore.Add(enemy);
        }

        public void MoveEnemy(
            string enemyId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed)
        {
            var enemy = _enemyStore.Get(enemyId);
            if(enemy != null)
            {
                enemy.PathToPoint(
                    position,
                    movementDestination,
                    movementSpeed);
                _enemyStore.Update(enemy);
            }
        }

        public void EngageEnemy(string enemyId, string targetId)
        {
            var enemy = _enemyStore.Get(enemyId);
            var player = _playerStore.Get(targetId);
            if(enemy != null && player != null)
            {
                enemy.EngageTarget(player.Id, player.Coordinates);
                _enemyStore.Update(enemy);
            }
        }

        public void DisengageEnemy(string enemyId)
        {
            var enemy = _enemyStore.Get(enemyId);
            if (enemy != null)
            {
                enemy.DisengageCharacter();
                _enemyStore.Update(enemy);
            }
        }

        public IEnumerable<EnemyEntity> GetEnemies()
        {
            return _enemyStore.GetAll().Values;
        }

        /// <summary>
        /// Create base enemy model
        /// </summary>
        /// <param name="enemyId">enemy id</param>
        /// <param name="enemyType">enemy type</param>
        /// <param name="position">position</param>
        /// <returns></returns>
        private EnemyEntity CreateEnemy(
            string enemyId,
            EnemyType enemyType,
            string targetId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed)
        {
            return new EnemyEntity(
                id: enemyId,
                type: enemyType,
                spawnModel: new SpawnModel
                {
                    IsAlive = true
                },
                pathingModel: new PathingModel
                {
                    MovementDestination = movementDestination,
                    EngageTargetId = targetId
                },
                characterModel: new CharacterModel
                {
                    Name = "Test"
                },
                collisionModel: new CollisionModel
                {
                    Bounds = new Bounds(10, 10)
                },
                movementModel: new MovementModel
                {
                    Coordinates = position,
                    MovementSpeed = movementSpeed,
                },
                combatModel: new CombatModel
                {
                    AttackRange = 30,
                    AttackSpeed = 1
                }) ;
        }
    }
}
