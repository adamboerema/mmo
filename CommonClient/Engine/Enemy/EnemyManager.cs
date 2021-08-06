using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;

namespace CommonClient.Engine.Enemy
{
    public class EnemyManager: IEnemyManager
    {
        private IEnemyStore _enemyStore;

        public EnemyManager(IEnemyStore enemyStore)
        {
            _enemyStore = enemyStore;
        }

        public void SpawnEnemy(
            string enemyId,
            EnemyType enemyType,
            Vector3 position)
        {
            var enemy = CreateEnemy(enemyId, enemyType, position);
            _enemyStore.Add(enemy);
        }

        public void MoveEnemy(
            string enemyId,
            Vector3 position,
            MovementType movementType,
            float movementSpeed)
        {
            var enemy = _enemyStore.Get(enemyId);
            enemy.Character.Coordinates = position;
            enemy.Character.MovementType = movementType;
            enemy.Character.MovementSpeed = movementSpeed;
            _enemyStore.Update(enemy);
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
            Vector3 position)
        {
            return new EnemyModel
            {
                Id = enemyId,
                Type = enemyType,
                Character = new CharacterModel
                {
                    Name = "Test",
                    IsAlive = true,
                    Coordinates = position,
                }
            };
        }
    }
}
