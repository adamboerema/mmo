using System;
using CommonClient.Components.Camera;
using CommonClient.Engine.Enemy;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components.Player
{
    public class EnemyDrawableComponent: DrawableGameComponent
    {
        private readonly ICamera _camera;
        private readonly IEnemyStore _enemyStore;
        private readonly IPlayerStore _playerStore;

        private Texture2D _enemyTexture;
        private SpriteBatch _spriteBatch;

        public EnemyDrawableComponent(Game game, ICamera camera): base(game)
        {
            _camera = camera;
            _enemyStore = GameServices.GetService<IEnemyStore>();
            _playerStore = GameServices.GetService<IPlayerStore>();
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
            foreach (var enemy in _enemyStore.GetAll().Values)
            {
                if(enemy.Movement.EngageTargetId != null)
                {
                    var player = _playerStore.Get(enemy.Movement.EngageTargetId);
                    if(player != null)
                    {
                        enemy.PathToPoint(
                            enemy.Character.Coordinates,
                            player.Coordinates,
                            enemy.Character.MovementSpeed);
                    }
                }
                var speed = enemy.Character.MovementSpeed;
                var movementSpeed = (float)gameTime.ElapsedGameTime.TotalMilliseconds * speed;
                enemy.Character.MoveToPoint(enemy.Movement.MovementDestination, movementSpeed);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());

            foreach(var enemy in _enemyStore.GetAll().Values)
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
