using System;
namespace Server.Engine
{
    public interface IGameComponent
    {
        public void Update(double elapsedTime, double timestamp);
    }
}
