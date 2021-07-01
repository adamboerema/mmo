using System;
using Common.Network.Packets.Auth;
using CommonClient;
using CommonClient.Container;
using DesktopClient.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace DesktopClient
{
    public class DesktopClient : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private IGameClient _gameClient;

        public DesktopClient()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            var configuration = new ClientConfiguration();
            GameServices.Initialize(configuration);
            _gameClient = GameServices.GetService<IGameClient>();
            _gameClient.Start();
            Login("test", "test12345");
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

        private void Login(string username, string password)
        {
            var packet = new LoginRequestPacket
            {
                Username = username,
                Password = password
            };
            _gameClient.Send(packet);
        }
    }
}
