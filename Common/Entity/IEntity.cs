using System;
using Common.Model.Shared;
using Common.Model.World;

namespace Common.Entity
{
    public interface IEntity
    {
        public void Update(GameTick gameTick, World world);
    }
}
