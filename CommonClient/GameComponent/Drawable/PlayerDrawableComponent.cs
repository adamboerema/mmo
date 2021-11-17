using System;
using CommonClient.GameComponent.Camera;
using CommonClient.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Common.Store;
using CommonClient.ComponentStore.Player;
using CommonClient.Engine.Player;

namespace CommonClient.GameComponent.Drawable
{
    public class PlayerDrawableComponent : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private ComponentStore<PlayerComponent> _playerStore;
        private IPlayerManager _playerManager;
        private Texture2D _clientPlayerTexture;
        private Texture2D _playerTexture;
        private ICamera _camera;

        public PlayerDrawableComponent(Game game, ICamera camera): base(game)
        {
            _camera = camera;
            _playerManager = GameServices.GetService<IPlayerManager>();
            _playerStore = GameServices.GetService<ComponentStore<PlayerComponent>>();
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var whiteSquare = new Color[100];
            for (var i = 0; i < 100; i++)
                whiteSquare[i] = Color.White;

            var greenSquare = new Color[100];
            for (var i = 0; i < 100; i++)
                greenSquare[i] = Color.Green;

            _clientPlayerTexture = new Texture2D(GraphicsDevice, 10, 10);
            _clientPlayerTexture.SetData(whiteSquare);

            _playerTexture = new Texture2D(GraphicsDevice, 10, 10);
            _playerTexture.SetData(greenSquare);
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            var clientPlayer = _playerManager.GetClientPlayer();
            if(clientPlayer != null)
            {
                var position = clientPlayer.Coordinates;
                var clientPosition = new Vector3(position.X, position.Y, 0);
                _camera.UpdatePosition(
                    clientPosition,
                    _clientPlayerTexture.Width,
                    _clientPlayerTexture.Height);
            }

            _playerManager.Update(gameTime.ToGameTick());
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());
            foreach(var player in _playerStore.GetAll().Values)
            {
                var texture = player.IsClient
                    ? _clientPlayerTexture
                    : _playerTexture;

                DrawPlayer(player, texture);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        /// <summary>
        /// Draw player with coordinates
        /// </summary>
        /// <param name="playerModel"></param>
        private void DrawPlayer(PlayerComponent player, Texture2D texture)
        {
            var coordinates = player.Coordinates;

            _spriteBatch.Draw(
                texture,
                new Vector2(coordinates.X, coordinates.Y),
                Color.White);
        }
    }
}
