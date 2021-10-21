using System;
using Common.Entity;
using Common.Model.Character;

namespace CommonClient.Engine.Player
{
    public class ClientPlayerEntity: PlayerEntity
    {
        public bool IsClient { get; init; }

        public ClientPlayerEntity(
            string id,
            bool isClient,
            CharacterModel characterModel,
            MovementModel movementModel) : base(
                id,
                characterModel,
                movementModel)
        {
            IsClient = isClient;
        }
    }
}
