using System;

namespace Common.Model
{
    public class CharacterModel
    {
        public string Name { get; set; }

        public MovementType MovementType { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
    }
}
