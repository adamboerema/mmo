using System;
using System.Numerics;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Store;
using Common.Utility;
using CommonClient.Component.Enemy;
using CommonClient.Component.Player;

namespace CommonClient.Engine.Enemy
{
    public class EnemyManager: IEnemyManager
    {
        private readonly ComponentStore<EnemyComponent> _enemyStore;
        private readonly ComponentStore<PlayerComponent> _playerStore;

        public EnemyManager(
            ComponentStore<EnemyComponent> enemyStore,
            ComponentStore<PlayerComponent> playerStore)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
        }

        public void Update(GameTick gameTime)
        {
            var world = WorldUtility.GetWorld();
            foreach(var enemy in _enemyStore.GetAll().Values)
            {
                enemy.Update(gameTime, world);
            }
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
                enemy.Disengage();
                _enemyStore.Update(enemy);
            }
        }

        /// <summary>
        /// Create base enemy model
        /// </summary>
        /// <param name="enemyId">enemy id</param>
        /// <param name="enemyType">enemy type</param>
        /// <param name="position">position</param>
        /// <returns></returns>
        private EnemyComponent CreateEnemy(
            string enemyId,
            EnemyType enemyType,
            string targetId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed)
        {
            return new EnemyComponent(
                new EnemyConfiguration
                {
                    Id = enemyId,
                    Type = enemyType,
                    Character = new CharacterModel
                    {
                        Name = "Enemy"
                    },
                    Pathing = new PathingModel
                    {
                        MovementDestination = movementDestination,
                        EngageTargetId = targetId
                    },
                    Movement = new MovementModel
                    {
                        Coordinates = position,
                        MovementSpeed = movementSpeed
                    },
                    Collision = new CollisionModel
                    {
                        Bounds = new Bounds(10, 10)
                    },
                    CombatModel = new CombatModel
                    {
                        AttackRange = 30,
                        AttackSpeed = 1
                    }
                });
        }
    }
}
