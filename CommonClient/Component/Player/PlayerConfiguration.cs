using System;
using Common.Model.Character;

namespace CommonClient.Component.Player
{
    public class PlayerConfiguration
    {
        public string Id { get; init; }

        public bool IsClient { get; set; }

        public CharacterModel Character { get; init; }

        public MovementModel Movement { get; init; }

    }
}
