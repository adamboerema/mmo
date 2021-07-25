using System;
using Microsoft.Xna.Framework;

namespace CommonClient.Components.Camera
{
    public interface ICamera
    {
        public Matrix GetPosition();

        public void UpdatePosition(Vector3 playerArea, int width, int height);
    }
}
