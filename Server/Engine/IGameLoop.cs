using System;
using Common.Model.Shared;

namespace Server.Engine
{
    public interface IGameLoop
    {
        public void Start();

        public void Update(GameTick gameTick);

        public void Stop();
    }
}
