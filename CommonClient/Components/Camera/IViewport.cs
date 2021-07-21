using System;
using Microsoft.Xna.Framework;

namespace CommonClient.Components.Camera
{
    public interface IViewport
    {
        /// <summary>
        /// Get center position in view port
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        public Vector3 GetCenterPosition(Vector3 position);
    }
}
