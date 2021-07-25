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

        public void UpdatePosition(Vector3 target, int width, int height)
        {
            _position = MoveTarget(target, width, height);
        }

        /// <summary>
        /// Moves target in designated 
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private Matrix MoveTarget(Vector3 target, int width, int height)
        {
            var scale = new Vector3(1, 1, 0);
            var translation = new Vector3(-target.X, -target.Y, 0);
            var viewport = new Vector3(_viewPort.View.Center.X, _viewPort.View.Center.Y, 0);

            var matrixScale = Matrix.CreateScale(scale);
            var matrixTranslation = Matrix.CreateTranslation(translation);
            var viewportTranslation = Matrix.CreateTranslation(viewport);

            var position = matrixScale *
                viewportTranslation *
                matrixTranslation;

            //return position;
            var clampedPosition = _viewPort.GetClampedViewport(position.Translation, width, height);
            return Matrix.CreateTranslation(clampedPosition);
        }
    }
}
