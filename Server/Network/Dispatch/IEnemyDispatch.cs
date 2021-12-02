using System;
using System.Numerics;
using Common.Model.Shared;

namespace Server.Network.Dispatch
{
    public interface IEnemyDispatch
    {
        /// <summary>
        /// Dispatches the enemy to an existing player
        /// </summary>
        /// <param name="playerId">Id of player</param>
        /// <param name="enemyId">Id of enemy</param>
        /// <param name="enemyType">Type of enemy</param>
        /// <param name="coordinates">Coordinates of the enemy</param>
        /// <param name="movementSpeed">Movement speed of the enemy</param>
        /// <param name="engageTargetId">Target id of the enemy</param>
        /// <param name="movementDestination">Movement destination of the enemy</param>

        public void DispatchEnemyToPlayer(
            string playerId,
            string enemyId,
            EnemyType enemyType,
            Vector3 coordinates,
            float movementSpeed,
            string engageTargetId,
            Vector3 movementDestination);

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

        /// <summary>
        /// Dispatch enemy attack
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetId"></param>
        /// <param name="damage"></param>
        public void DispatchEnemyAttack(
            string id,
            string targetId,
            int damage);
    }
}
