using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;
using Common.Utility;

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
            Vector3 movementDestination,
            float movementSpeed)
        {
            var enemy = _enemyStore.Get(enemyId);
            enemy.Character.Coordinates = position;
            enemy.MovementDestination = movementDestination;
            enemy.Character.MovementSpeed = movementSpeed;
            enemy.Character.MovementType = MovementUtility.GetDirectionToPoint(position, movementDestination);
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
