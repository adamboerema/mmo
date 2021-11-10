using System;
using Common.Model.Shared;
using Common.Model.World;

namespace Common.Component
{
    public interface IComponent
    {
        public void Update(GameTick gameTick, World world);
    }
}
