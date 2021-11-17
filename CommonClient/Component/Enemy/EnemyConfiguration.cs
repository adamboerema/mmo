using System;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;

namespace CommonClient.ComponentStore.Enemy
{
    public class EnemyConfiguration
    {
        public string Id { get; init; }

        public EnemyType Type { get; init; }

        public PathingModel Pathing { get; init; }

        public MovementModel Movement { get; init; }

        public CharacterModel Character { get; init; }

        public CollisionModel Collision { get; init; }

        public CombatModel CombatModel { get; init; }

        public SpawnModel SpawnModel { get; init; }
    }
}
