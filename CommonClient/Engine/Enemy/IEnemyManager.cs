using System;
using System.Collections.Generic;
using System.Numerics;
using Common.Base;

namespace CommonClient.Engine.Enemy
{
    public interface IEnemyManager
    {
        /// <summary>
        ///  Spawn the enemy
        /// </summary>
        /// <param name="enemyId"></param>
        /// <param name="enemyType"></param>
        /// <param name="targetId"></param>
        /// <param name="position"></param>
        /// <param name="movementDestination"></param>
        /// <param name="movementSpeed"></param>
        public void SpawnEnemy(
            string enemyId,
            EnemyType enemyType,
            string targetId,
            Vector3 position,
            Vector3 movementDestination,
            float movementSpeed);

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
        /// Starts enemy engagement to a target
        /// </summary>
        /// <param name="enemyId">Id of the enemy engaging</param>
        /// <param name="targetId">Id of the enemy's target</param>
        public void EngageEnemy(
            string enemyId,
            string targetId);

        /// <summary>
        /// Disengage enemy from target
        /// </summary>
        /// <param name="enemyId">Id of enemy</param>
        public void DisengageEnemy(string enemyId);

        /// <summary>
        /// Get list of enemies
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EnemyEntity> GetEnemies();
    }
}
