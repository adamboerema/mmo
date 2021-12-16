using System;
using Common.Model.Behavior;
using Common.Model.Character;

namespace CommonClient.ComponentStore.Player
{
    public class PlayerConfiguration
    {
        public string Id { get; init; }

        public bool IsClient { get; set; }

        public CharacterModel Character { get; init; }

        public MovementModel Movement { get; init; }

        public CollisionModel Collision { get; init; }
    }
}
