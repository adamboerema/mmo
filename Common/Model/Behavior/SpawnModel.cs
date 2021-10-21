using System;
using System.Drawing;
using System.Numerics;
using Common.Model.Shared;

namespace Common.Model.Behavior
{
    public class SpawnModel
    {
        public bool IsAlive { get; set; } = true;

        public int RespawnSeconds { get; set; } = 60;

        public Rectangle SpawnArea { get; set; }

        public double SpawnTime { get; set; } = DateTimeOffset.Now.ToUnixTimeSeconds();

        public double DeathTime { get; set; }


        /// <summary>
        /// Gets a random spawn point within a rectangle spawn area
        /// </summary>
        /// <param name="spawnArea">Spawn area</param>
        /// <returns></returns>
        public Vector3 GetRandomSpawnPoint()
        {
            var random = new Random();
            return new Vector3(
                random.Next(SpawnArea.Left, SpawnArea.Right),
                random.Next(SpawnArea.Top, SpawnArea.Bottom),
                0);
        }

        /// <summary>
        /// Has enough time passed for the respawn
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public bool ShouldRespawn(GameTick gameTime)
        {
            var respawnTime = DeathTime + RespawnSeconds;
            return !IsAlive && respawnTime < gameTime.Timestamp;
        }
    }
}
