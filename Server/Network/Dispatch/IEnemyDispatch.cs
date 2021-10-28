using System;
using System.Numerics;
using Common.Model.Shared;

namespace Server.Network.Dispatch
{
    public interface IEnemyDispatch
    {
        /// <summary>
        /// Dispatch when an enemy spawns
        /// </summary>
        /// <param name="enemy"></param>
        public void DispatchEnemySpawn(
            string id,
            EnemyType enemyType,
            Vector3 coordinates);

        /// <summary>
        /// Dispatch when an enemy moves
        /// </summary>
        /// <param name="enemy"></param>
        public void DispatchEnemyMovement(
            string id,
            Vector3 coordinates,
            Vector3 movementDestination,
            float movementSpeed);

        /// <summary>
        /// Dispatch enemy engagement
        /// </summary>
        /// <param name="enemy"></param>
        public void DispatchEnemyEngage(
            string id,
            string targetId);

        /// <summary>
        /// Dispatch enemy disengagement
        /// </summary>
        /// <param name="enemy"></param>
        public void DispatchEnemyDisenage(string id);
       
    }
}
