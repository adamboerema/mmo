using System;
using Common.Model;

namespace CommonClient.Engine.Player
{
    public interface IPlayersStore
    {
        public ClientPlayerModel Get(string playerId);

        public void Add(ClientPlayerModel playerModel);

        public void Update(ClientPlayerModel playerModel);

        public void Remove(string playerId);

        public void UpdateMovement(string playerId, int x, int y, int z, MovementType movementType);
    }
}
