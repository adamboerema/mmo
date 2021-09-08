using System;
using System.Numerics;
using Common.Base;
using Common.Model;

namespace CommonClient.Engine.Player
{
    public class ClientPlayerModel: PlayerModel
    {
        public ClientPlayerModel(
            string id,
            bool isClient,
            CharacterModel character) : base(
                id,
                character)
        {
            IsClient = isClient;
        }

        public bool IsClient { get; private set; }
    }
}
