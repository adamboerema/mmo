using System;
using Common.Model.Character;

namespace Common.Model.Behavior
{
    public class CollisionModel
    {
        public Bounds Bounds { get; set; }

        /// <summary>
        /// Get the distance to collide
        /// </summary>
        /// <returns></returns>
        public float GetCollisionDistance() => (float) Bounds.Radius;
    }
}
