using System;
using System.Numerics;

namespace Common.Model
{
    public class CharacterModel
    {
        public string Name { get; set; }

        public MovementType MovementType { get; set; }
        public Vector3 Coordinates { get; set; }
    }
}
