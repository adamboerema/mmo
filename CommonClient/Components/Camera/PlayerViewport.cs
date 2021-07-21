using System;
using Microsoft.Xna.Framework;

namespace CommonClient.Components.Camera
{
    public class PlayerViewport : IViewport
    {
        private readonly Rectangle _view;
        private readonly Rectangle _maxView;

        public PlayerViewport(
            Rectangle View,
            Rectangle MaxView)
        {
            _view = View;
            _maxView = MaxView;
        }

        public Vector3 GetCenterPosition(Vector3 position)
        {
            var clampedPosition = ClampToMaxArea(position);
            var centerX = clampedPosition.X -= _view.Width / 2;
            var centerY = clampedPosition.Y -= _view.Height / 2;
            return new Vector3(centerX, centerY, 0);
        }

        public Vector3 ClampToMaxArea(Vector3 position)
        {
            var clampedX = Math.Clamp(position.X, _maxView.Left, _maxView.Right);
            var clampedY = Math.Clamp(position.Y, _maxView.Top, _maxView.Bottom);
            return new Vector3(clampedX, clampedY, 0);
        }

    }
}
