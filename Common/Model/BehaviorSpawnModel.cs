using System;
using System.Drawing;

namespace Common.Model
{
    public class BehaviorSpawnModel
    {
        public int RespawnSeconds { get; set; } = 60;

        public Rectangle SpawnArea { get; set; }

        public double SpawnTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public double DeathTime { get; set; }
    }
}
