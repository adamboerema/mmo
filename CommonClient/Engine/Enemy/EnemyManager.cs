using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Base;
using Common.Model.Behavior;
using Common.Model.Character;
using CommonClient.Engine.Player;

namespace CommonClient.Engine.Enemy
{
    public class EnemyManager: IEnemyManager
    {
        private readonly IEnemyStore _enemyStore;
        private readonly IPlayerStore _playerStore;

        public EnemyManager(
            IEnemyStore enemyStore,
            IPlayerStore playerStore)
        {
            _enemyStore = enemyStore;
            _playerStore = playerStore;
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
                enemy.EngageCharacter(player.Id, player.Coordinates);
                _enemyStore.Update(enemy);
            }
        }

        public void DisengageEnemy(string enemyId)
        {
            var enemy = _enemyStore.Get(enemyId);
            if (enemy != null)
            {
                enemy.DisengagePlayer();
                _enemyStore.Update(enemy);
            }
        }

        public IEnumerable<EnemyModel> GetEnemies()
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
        private EnemyModel CreateEnemy(
            string enemyId,
            EnemyType enemyType,
            string targetId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed)
        {
            return new EnemyModel(
                id: enemyId,
                type: enemyType,
                spawnModel: new SpawnModel
                {
                    IsAlive = true
                },
                movementModel: new MovementModel
                {
                    MovementDestination = movementDestination,
                    EngageTargetId = targetId
                },
                characterModel: new CharacterModel
                {
                    Name = "Test",
                    Coordinates = position,
                    MovementSpeed = movementSpeed,
                    Bounds = new Bounds(10, 10)
                },
                combatModel: new CombatModel
                {
                    AttackRange = 30,
                    AttackSpeed = 1
                }) ;
        }
    }
}
