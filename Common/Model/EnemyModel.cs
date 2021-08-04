using System;
using System.Drawing;

namespace Common.Model
{
    public class EnemyModel
    {
        public string Id { get; set; }

        public EnemyType Type { get; set; }

        public double SpawnTime { get; set; }

        public double DeathTime { get; set; }

        public double LastMovementTime { get; set; }

        public int RespawnSeconds { get; set; }

        public int MovementSeconds { get; set; }

        public Rectangle SpawnArea { get; set; }

        public Rectangle MovementArea { get; set; }

        public CharacterModel Character { get; set; }
    }
}
