using System;
using System.Numerics;
using Common.Base;

namespace Common.Model.Base
{
    public abstract class BaseCharacterModel
    {
        public string Name { get; set; }

        public bool IsAlive { get; set; }

        public float MovementSpeed { get; set; }

        public MovementType MovementType { get; set; }

        public Vector3 Coordinates { get; set; }
    }
}
