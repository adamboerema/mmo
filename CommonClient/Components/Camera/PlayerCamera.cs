using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Camera
{
    public class PlayerCamera: ICamera
    {
        private Matrix _position = Matrix.Identity;
        private Rectangle _cameraArea;

        public PlayerCamera(Rectangle cameraArea)
        {
            _cameraArea = cameraArea;
        }

        public Matrix GetPosition()
        {
            return _position;
        }

        public void UpdatePosition(Vector2 position)
        {
            var centerPosition = GetCenterFromPosition(position, _cameraArea);
            _position = MoveTarget(centerPosition);
        }

        private Matrix MoveTarget(Vector2 target)
        {
            var scale = new Vector3(1, 1, 0);
            var translation = new Vector3(-target.X, -target.Y, 0);

            var matrixScale = Matrix.CreateScale(scale);
            var matrixTranslation = Matrix.CreateTranslation(translation);

            return matrixScale * matrixTranslation;
        }

        private Vector2 GetCenterFromPosition(Vector2 position, Rectangle cameraArea)
        {
            var centerX = position.X -= cameraArea.Width / 2;
            var centerY = position.Y -= cameraArea.Height / 2;
            return new Vector2(centerX, centerY);
        }
    }
}
