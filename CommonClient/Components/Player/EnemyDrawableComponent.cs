using System;
using Common.Extensions;
using CommonClient.Components.Camera;
using CommonClient.Engine.Enemy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Player
{
    public class EnemyDrawableComponent: DrawableGameComponent
    {
        private const int WORLD_HEIGHT = 1000;
        private const int WORLD_WIDTH = 1000;

        private readonly ICamera _camera;
        private readonly IEnemyManager _enemyManager;

        private Texture2D _enemyTexture;
        private SpriteBatch _spriteBatch;

        public EnemyDrawableComponent(Game game, ICamera camera): base(game)
        {
            _camera = camera;
            _enemyManager = GameServices.GetService<IEnemyManager>();
        }

        public override void Initialize()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            var redSquare = new Color[100];
            for (var i = 0; i < 100; i++)
                redSquare[i] = Color.Red;

            _enemyTexture = new Texture2D(GraphicsDevice, 10, 10);
            _enemyTexture.SetData(redSquare);

            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (var enemy in _enemyManager.GetEnemies())
            {
                var speed = enemy.Character.MovementSpeed;
                var movementSpeed = (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
                enemy.Character.MoveCoordinates(movementSpeed, WORLD_HEIGHT, WORLD_WIDTH);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());

            foreach(var enemy in _enemyManager.GetEnemies())
            {
                var coordinates = enemy.Character.Coordinates;
                _spriteBatch.Draw(
                    _enemyTexture,
                    new Vector2(coordinates.X, coordinates.Y),
                    Color.White);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
