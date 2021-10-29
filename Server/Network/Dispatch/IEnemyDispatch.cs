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
        /// <param name="id">Id of enemy spawn</param>
        /// <param name="enemyType">type of enemy</param>
        /// <param name="coordinates">coordinates of spawn</param>
        public void DispatchEnemySpawn(
            string id,
            EnemyType enemyType,
            Vector3 coordinates);

        /// <summary>
        /// Dispatch enemy movement
        /// </summary>
        /// <param name="id">enemy id</param>
        /// <param name="coordinates">coordinates of enemy</param>
        /// <param name="movementDestination">coordinates of enemy destination</param>
        /// <param name="movementSpeed">movement speed of enemy</param>
        public void DispatchEnemyMovement(
            string id,
            Vector3 coordinates,
            Vector3 movementDestination,
            float movementSpeed);

        /// <summary>
        /// Dispatch enemy engage
        /// </summary>
        /// <param name="id">Enemy id</param>
        /// <param name="targetId">Target Id</param>
        public void DispatchEnemyEngage(
            string id,
            string targetId);

        /// <summary>
        /// Dispatch enemy disengage
        /// </summary>
        /// <param name="id"></param>
        public void DispatchEnemyDisenage(string id);
       
    }
}
