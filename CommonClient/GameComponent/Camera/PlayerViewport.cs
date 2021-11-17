using System;
using Microsoft.Xna.Framework;

namespace CommonClient.GameComponent.Camera
{
    public class PlayerViewport : IViewport
    {
        public Rectangle View { get; set; }

        public Rectangle PlayerView { get; set; }

        public Rectangle WorldView { get; set; }

        public Vector3 GetClampedViewport(Vector3 position, int offsetWidth, int offsetHeight)
        {
            var positionToWorld = Vector3.Negate(position);
            if(!IsInCorner(positionToWorld))
            {
                return position;
            }
            else
            {
                var adjustedAreaWidth = WorldView.Width - View.Width + offsetWidth;
                var adjustedAreaHeight = WorldView.Height - View.Height + offsetHeight;
                var clampedX = Math.Clamp(positionToWorld.X, 0, adjustedAreaWidth);
                var clampedY = Math.Clamp(positionToWorld.Y, 0, adjustedAreaHeight);
                return new Vector3(-clampedX, -clampedY, 0);
            }
        }

        public bool IsInCorner(Vector3 position)
        {

            var offsetX = WorldView.Width - View.Width;
            var offsetY = WorldView.Height - View.Height;

            return (position.X < 0 || position.X > offsetX)
                || (position.Y < 0 || position.Y > offsetY);
        }
    }
}
