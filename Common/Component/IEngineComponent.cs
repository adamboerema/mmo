using System;
using Common.Model.Shared;

namespace Common.Component
{
    public interface IEngineComponent
    {
        public void Update(GameTick gameTime);
    }
}
