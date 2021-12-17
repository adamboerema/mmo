using System;
using CommonClient.GameComponent.Camera;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CommonClient.Extensions;

namespace CommonClient.GameComponent.Drawable
{
    public class WorldDrawableComponent: DrawableGameComponent
    {
        private ICamera _camera;
        private SpriteBatch _spriteBatch;
        private Texture2D _background;

        public WorldDrawableComponent(Game game, ICamera camera): base (game)
        {
            _camera = camera;
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        override protected void LoadContent()
        {
            _background = Game.Content.Load<Texture2D>("environment/bg_rock_dirt");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());

            DrawArea(_spriteBatch, _background);

            _spriteBatch.End();

            base.Draw(gameTime);
        }


        public void DrawArea(SpriteBatch spriteBatch, Texture2D tile)
        {
            var scaleHeight = 200;
            var scaleWidth = 200;
            var textureScale = 0.5f;

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    var xCoordinate = i * scaleWidth;
                    var yCoordinate = j * scaleHeight;

                    var drawPoint = new Vector2(xCoordinate, yCoordinate);

                    spriteBatch.Draw(
                        tile,
                        drawPoint,
                        null,
                        Color.White,
                        0,
                        Vector2.Zero,
                        textureScale,
                        SpriteEffects.None,
                        0);
                }
            }
        }
    }
}
