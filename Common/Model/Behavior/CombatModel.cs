using System;
using System.Numerics;
using Common.Utility;

namespace Common.Model.Behavior
{
    public class CombatModel
    {
        public bool IsAttacking { get; set; } = false;

        public double LastAttackTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public float AttackRange { get; init; }

        public float AttackSpeed { get; init; }

        /// <summary>
        /// Get Attack distance to engage target
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        public float GetAttackDistance(float offset) => AttackRange + offset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="location"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public void ShouldAttack(Vector3 location, double timestamp)
        {
        }
    }
}
