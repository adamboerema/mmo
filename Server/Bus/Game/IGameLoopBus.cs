using System;
using Common.Bus;

namespace Server.Bus.Game
{
    public interface IGameLoopBus: IEventBus<GameLoopEvent>
    {
        public void Publish(double elapsedTime);
    }
}
