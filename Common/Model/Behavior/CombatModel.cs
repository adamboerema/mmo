using System;
using System.Numerics;
using Common.Model.Shared;
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
        /// Should entity attack target
        /// </summary>
        /// <param name="location"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public bool ShouldAttack(double timestamp)
        {
            var attackTime = LastAttackTime + AttackSpeed;
            return timestamp > attackTime;
        }

        /// <summary>
        /// Start attacking the target
        /// </summary>
        public void Attack()
        {
            IsAttacking = true;
            LastAttackTime = DateTimeOffset.Now.ToUnixTimeSeconds();
        }

        /// <summary>
        /// Get the attack damage
        /// </summary>
        /// <param name="weaponType"></param>
        /// <returns></returns>
        public int GetAttackDamage(WeaponType weaponType)
        {
            // TODO calculate damage here
            return 10;
        }
    }
}
