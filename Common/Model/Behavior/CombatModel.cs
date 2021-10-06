using System;
namespace Common.Model.Behavior
{
    public class CombatModel
    {
        public bool IsAttacking { get; set; } = false;

        public double LastAttackTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public float AttackRange { get; init; }

        public float AttackSpeed { get; init; }
    }
}
