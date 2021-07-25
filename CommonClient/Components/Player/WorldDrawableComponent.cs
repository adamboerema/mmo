using System;
using CommonClient.Components.Camera;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Player
{
    public class WorldDrawableComponent: DrawableGameComponent
    {
        private const int WORLD_HEIGHT = 1000;
        private const int WORLD_WIDTH = 1000;

        private readonly int[,] _worldArea;

        private readonly IPlayerManager _playerManager;

        private ICamera _camera;
        private SpriteBatch _spriteBatch;
        private Texture2D _background;

        public WorldDrawableComponent(Game game, ICamera camera): base (game)
        {
            _camera = camera;
            _playerManager = GameServices.GetService<IPlayerManager>();
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            base.Initialize();
        }

        override protected void LoadContent()
        {
            _background = Game.Content.Load<Texture2D>("bg_rock_dirt");
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());

            DrawArea(_spriteBatch, _background);

            _spriteBatch.End();
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
