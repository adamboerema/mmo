using System;
using Common.Model.Shared;

namespace Server.Engine
{
    public interface IGameComponent
    {
        public void Update(GameTick gameTime);
    }
}
