using System;
namespace Common.Model.Shared
{
    public struct GameTick
    {
        public double ElapsedTime { get;}

        public double Timestamp { get; }

        public GameTick(
            double elapsedTime,
            double timestamp)
        {
            ElapsedTime = elapsedTime;
            Timestamp = timestamp;
        }
    }
}
