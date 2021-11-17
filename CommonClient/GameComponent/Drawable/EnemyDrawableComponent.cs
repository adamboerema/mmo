using System;
using CommonClient.GameComponent.Camera;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using CommonClient.ComponentStore.Enemy;
using Common.Store;
using CommonClient.Engine.Enemy;
using CommonClient.Extensions;

namespace CommonClient.GameComponent
{
    public class EnemyDrawableComponent: DrawableGameComponent
    {
        private readonly ICamera _camera;
        private readonly IEnemyManager _enemyManager;
        private readonly ComponentStore<EnemyComponent> _enemyStore;

        private Texture2D _enemyTexture;
        private SpriteBatch _spriteBatch;

        public EnemyDrawableComponent(Game game, ICamera camera): base(game)
        {
            _camera = camera;
            _enemyStore = GameServices.GetService<ComponentStore<EnemyComponent>>();
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
            _enemyManager.Update(gameTime.ToGameTick());
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(transformMatrix: _camera.GetPosition());

            foreach(var enemy in _enemyStore.GetAll().Values)
            {
                var coordinates = enemy.Coordinates;
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
