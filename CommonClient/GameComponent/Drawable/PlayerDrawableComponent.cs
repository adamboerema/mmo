﻿using System;
using Common.Utility;
using CommonClient.GameComponent.Camera;
using CommonClient.Engine.Player;
using CommonClient.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.GameComponent.Player
{
    public class PlayerDrawableComponent : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private IPlayerManager _playerManager;
        private Texture2D _clientPlayerTexture;
        private Texture2D _playerTexture;
        private ICamera _camera;

        public PlayerDrawableComponent(Game game, ICamera camera): base(game)
        {
            _camera = camera;
            _playerManager = GameServices.GetService<IPlayerManager>();
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
            var players = _playerManager.GetPlayers();

            foreach (var player in players)
            {
                if (player.IsClient)
                {
                    var position = player.Coordinates;
                    var vectorPosition = new Vector3(position.X, position.Y, 0);
                    _camera.UpdatePosition(
                        vectorPosition,
                        _clientPlayerTexture.Width,
                        _clientPlayerTexture.Height);
                }

                player.Update(gameTime.ToGameTick(), WorldUtility.GetWorld());
                _playerManager.UpdatePlayer(player);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());
            var players = _playerManager.GetPlayers();
            foreach(var player in players)
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
        private void DrawPlayer(ClientPlayerEntity playerModel, Texture2D texture)
        {
            var coordinates = playerModel.Coordinates;

            _spriteBatch.Draw(
                texture,
                new Vector2(coordinates.X, coordinates.Y),
                Color.White);
        }
    }
}