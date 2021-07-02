using System;
using Common.Model;

namespace CommonClient.Engine.Player
{
    public interface IPlayerStore
    {
        public PlayerModel Get(string playerId);

        public void Add(PlayerModel playerModel);

        public void Update(PlayerModel playerModel);

        public void Remove(string playerId);

        public void UpdateMovement(string playerId, int x, int y, int z, MovementType movementType);
    }
}
