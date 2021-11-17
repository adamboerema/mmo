using System;
using CommonClient;
using CommonClient.GameComponent;
using CommonClient.GameComponent.Camera;
using CommonClient.GameComponent.Drawable;
using CommonClient.GameComponent.Input;
using DesktopClient.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DesktopClient
{
    public class DesktopClient : Game
    {
        private const int WORLD_HEIGHT = 1000;
        private const int WORLD_WIDTH = 1000;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IGameClient _gameClient;
        private ICamera _camera;

        public DesktopClient()
        {
            var configuration = new ClientConfiguration();
            _gameClient = new GameClient(configuration);

            _graphics = new GraphicsDeviceManager(this);

            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var viewArea = GraphicsDevice.Viewport.Bounds;
            var maxArea = new Rectangle(0, 0, WORLD_WIDTH, WORLD_HEIGHT);
            _camera = new PlayerCamera(new PlayerViewport
            {
                View = viewArea,
                WorldView = maxArea
            });

            var movementComponent = new InputComponent(this);
            var playerDrawableComponent = new PlayerDrawableComponent(this, _camera);
            var worldDrawableComponent = new WorldDrawableComponent(this, _camera);
            var enemyDrawableComponent = new EnemyDrawableComponent(this, _camera);
            Components.Add(movementComponent);
            Components.Add(worldDrawableComponent);
            Components.Add(playerDrawableComponent);
            Components.Add(enemyDrawableComponent);

            _gameClient.Start();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed
                || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(gameTime);
        }
    }
}
