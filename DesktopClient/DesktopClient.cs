using System;
using Common.Packets.ClientToServer.Auth;
using CommonClient;
using CommonClient.Bus.Packet;
using CommonClient.Components.Movement;
using CommonClient.Components.Player;
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
        private IDispatchPacketBus _dispatchPacketBus;

        public DesktopClient()
        {
            var configuration = new ClientConfiguration();
            _gameClient = new GameClient(configuration);

            _graphics = new GraphicsDeviceManager(this);
            _dispatchPacketBus = GameServices.GetService<IDispatchPacketBus>();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            var movementComponent = new MovementComponent(this);
            var playerDrawableComponent = new PlayerDrawableComponent(this);
            Components.Add(movementComponent);
            Components.Add(playerDrawableComponent);
        }

        protected override void Initialize()
        {
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
            _dispatchPacketBus.Publish(packet);
        }
    }
}
