using System;
using System.Drawing;

namespace Common.Model
{
    public class EnemyModel
    {
        public string Id { get; set; }

        public int TimeToSpawn { get; set; }

        public int SpawnTime { get; set; }

        public Rectangle SpawnArea { get; set; }

        public CharacterModel Character { get; set; }
    }
}
