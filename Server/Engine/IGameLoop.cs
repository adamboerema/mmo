using System;
namespace Server.Engine
{
    public interface IGameLoop
    {
        public void Start();

        public void Update(double elapsedTime);

        public void Stop();
    }
}
