using System;
using System.Numerics;
using Common.Base;

namespace CommonClient.Engine.Player
{
    public class ClientPlayerModel: PlayerModel
    {
        public ClientPlayerModel(
            string id,
            string name,
            bool isClient,
            Direction direction,
            Vector3 coordinates,
            bool isMoving,
            float movementSpeed) : base(
                id,
                name,
                direction,
                coordinates,
                isMoving,
                movementSpeed)
        {
            IsClient = isClient;
        }

        public bool IsClient { get; private set; }
    }
}
