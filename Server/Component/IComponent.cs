using System;
using Common.Model.Shared;
using Common.Model.World;

namespace Server.Component
{
    public interface IComponent
    {
        public void Update(GameTick gameTick, World world);
    }
}
