using System;
namespace Server.Engine
{
    public interface IGameLoopEvent
    {
        public void Update(double elapsedTime, double timestamp);
    }
}
