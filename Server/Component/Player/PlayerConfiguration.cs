using System;
using Common.Model.Character;

namespace Server.Component.Player
{
    public class PlayerConfiguration
    {
        public string Id { get; init; }

        public bool IsClient { get; init; }

        public CharacterModel Character { get; init; }

        public MovementModel Movement { get; init; }
    }
}
