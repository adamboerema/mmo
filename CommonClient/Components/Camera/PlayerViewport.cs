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
            position.X -= _view.Width / 2;
            position.Y -= _view.Height / 2;
            var clampedPosition = ClampToMaxArea(position);
            return new Vector3(clampedPosition.X, clampedPosition.Y, 0);
        }

        /// <summary>
        /// Clamp the coordinates to the bounds of the world
        /// </summary>
        /// <param name="position"></param>
        /// <returns></returns>
        private Vector3 ClampToMaxArea(Vector3 position)
        {
            var clampedX = Math.Clamp(position.X, _maxView.Left, _maxView.Right);
            var clampedY = Math.Clamp(position.Y, _maxView.Top, _maxView.Bottom);
            return new Vector3(clampedX, clampedY, 0);
        }

    }
}
