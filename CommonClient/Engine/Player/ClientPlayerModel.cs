using System;
using Common.Base;
using Common.Model.Character;

namespace CommonClient.Engine.Player
{
    public class ClientPlayerModel: PlayerModel
    {
        public bool IsClient { get; init; }

        public ClientPlayerModel(
            string id,
            bool isClient,
            CharacterModel character) : base(
                id,
                character)
        {
            IsClient = isClient;
        }
    }
}
