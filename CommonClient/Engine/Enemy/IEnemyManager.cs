using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Model;

namespace CommonClient.Engine.Enemy
{
    public interface IEnemyManager
    {
        /// <summary>
        /// Spawn the enemy
        /// </summary>
        /// <param name="enemyId"></param>
        /// <param name="enemyType"></param>
        /// <param name="position"></param>
        public void SpawnEnemy(
            string enemyId,
            EnemyType enemyType,
            Vector3 position);

        /// <summary>
        /// Move an enemy
        /// </summary>
        /// <param name="enemyId">string enemy id</param>
        /// <param name="position">vector3 position</param>
        /// <param name="movementDestination">movement destination</param>
        /// <param name="movementSpeed">movement speed</param>
        public void MoveEnemy(
            string enemyId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed);

        /// <summary>
        /// Get list of enemies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EnemyModel> GetEnemies();
    }
}
