using System;
using Common.Model.Shared;
using Common.Model.World;

namespace Common.Component
{
    public interface IComponent
    {
        public string Id { get; init; }

        public void Update(GameTick gameTick, World world);
    }
}
