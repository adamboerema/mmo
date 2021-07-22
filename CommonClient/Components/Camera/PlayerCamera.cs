using System;
using Microsoft.Xna.Framework;

namespace CommonClient.Components.Camera
{
    public class PlayerCamera: ICamera
    {
        private Matrix _position = Matrix.Identity;
        private IViewport _viewPort;

        public PlayerCamera(IViewport viewPort)
        {
            _viewPort = viewPort;
        }

        public Matrix GetPosition()
        {
            return _position;
        }

        public void UpdatePosition(Vector3 position)
        {
            var centerPosition = _viewPort.GetCenterPosition(position);
            _position = MoveTarget(centerPosition);
        }

        private Matrix MoveTarget(Vector3 target)
        {
            var scale = new Vector3(1, 1, 0);
            var translation = new Vector3(-target.X, -target.Y, 0);

            var matrixScale = Matrix.CreateScale(scale);
            var matrixTranslation = Matrix.CreateTranslation(translation);

            return matrixScale * matrixTranslation;
        }
    }
}
