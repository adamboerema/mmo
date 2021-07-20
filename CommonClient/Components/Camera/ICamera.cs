using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Camera
{
    public interface ICamera
    {
        public Matrix GetPosition();

        public void UpdatePosition(Vector2 position);
    }
}
