using System;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Player
{
    public class PlayerDrawableComponent : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private IPlayerManager _playerManager;
        private ClientPlayerModel _playerModel;
        private Texture2D _clientPlayerTexture;
        private Texture2D _playerTexture;

        public PlayerDrawableComponent(Game game): base(game)
        {
            _playerManager = GameServices.GetService<IPlayerManager>();
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var whiteSquare = new Color[100];
            for (var i = 0; i < 100; i++)
                whiteSquare[i] = Color.White;
                

            _clientPlayerTexture = new Texture2D(GraphicsDevice, 10, 10);
            _clientPlayerTexture.SetData(whiteSquare);

            _playerTexture = new Texture2D(GraphicsDevice, 1, 1);
            _playerTexture.SetData(new Color[] { Color.Green });
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            var players = _playerManager.GetPlayers();
            foreach(var (_, player) in players)
            {
                if(player.IsClient)
                {
                    DrawClientPlayer();
                }
                else
                {
                    DrawPlayer(player);
                }
                
            }
            base.Draw(gameTime);
            _spriteBatch.End();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="playerModel"></param>
        private void DrawPlayer(ClientPlayerModel playerModel)
        {
            var coordinates = playerModel.Character.Coordinates;
            var viewPort = GraphicsDevice.Viewport;
            if (viewPort.Bounds.Contains(coordinates.X, coordinates.Y)) {
                _spriteBatch.Draw(
                    _playerTexture,
                    new Vector2(coordinates.X, coordinates.Y),
                    Color.Green);
            }
        }

        /// <summary>
        /// Draw Client Player in the center
        /// </summary>
        private void DrawClientPlayer()
        {
            var viewportWidth = GraphicsDevice.Viewport.Width;
            var viewportHeight = GraphicsDevice.Viewport.Height;
            var centerWidth = (viewportWidth / 2) - (_clientPlayerTexture.Width / 2);
            var centerHeight = (viewportHeight / 2) - (_clientPlayerTexture.Height / 2);

            _spriteBatch.Draw(
                _clientPlayerTexture,
                new Vector2(centerHeight, centerWidth),
                Color.White);
        }
    }
}
