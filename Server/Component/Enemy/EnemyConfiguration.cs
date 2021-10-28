using System;
using Common.Model.Behavior;
using Common.Model.Character;
using Common.Model.Shared;

namespace Server.Component.Enemy
{
    public class EnemyConfiguration
    {
        public string Id { get; init;  }

        public EnemyType Type { get; init; }

        public SpawnModel Spawn { get; init; }

        public PathingModel Pathing { get; init; }

        public MovementModel Movement { get; init; }

        public CharacterModel Character { get; init; }

        public CombatModel Combat { get; init; }

        public CollisionModel Collision { get; init; }

    }
}
