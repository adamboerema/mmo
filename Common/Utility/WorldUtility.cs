using System;
using Common.Model.World;

namespace Common.Utility
{
    public static class WorldUtility
    {
        public static World GetWorld()
        {
            return new World
            {
                Height = 10000,
                Width = 10000
            };
        }
    }
}
