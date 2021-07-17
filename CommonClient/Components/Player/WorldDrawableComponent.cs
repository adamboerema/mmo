using System;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Player
{
    public class WorldDrawableComponent: DrawableGameComponent
    {
        private readonly IPlayerManager _playerManager;

        private SpriteBatch _spriteBatch;
        private Texture2D _background;
        private Rectangle _world;

        public WorldDrawableComponent(Game game): base (game)
        {
            _playerManager = GameServices.GetService<IPlayerManager>();
            _world = new Rectangle(0, 0, 10000, 10000);
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

        public override void Update(GameTime gameTime)
        {

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            var viewportWidth = GraphicsDevice.Viewport.Width;
            var viewportHeight = GraphicsDevice.Viewport.Height;
            var centerWidth = viewportWidth / 2;
            var centerHeight = viewportHeight / 2;

            _spriteBatch.Begin();

            _spriteBatch.Draw(
                _background,
                new Vector2(centerWidth, centerHeight),
                Color.White);

            _spriteBatch.End();
        }
    }
}
