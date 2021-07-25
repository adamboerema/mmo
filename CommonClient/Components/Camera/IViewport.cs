using System;
using Microsoft.Xna.Framework;

namespace CommonClient.Components.Camera
{
    public interface IViewport
    {
        /// <summary>
        /// Current viewport view
        /// </summary>
        public Rectangle View { get; set; }

        /// <summary>
        /// Maximum range of viewport
        /// </summary>
        public Rectangle WorldView { get; set; }

        /// <summary>
        /// Clamps the position to the world coordinates
        /// </summary>
        /// <param name="area">Area to clamp</param>
        /// <returns></returns>
        public Vector3 GetClampedViewport(Vector3 position, int offsetWidth, int offsetHeight);

    }
}
