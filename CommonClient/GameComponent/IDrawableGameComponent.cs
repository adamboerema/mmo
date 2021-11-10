using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.GameComponent
{
    public interface IDrawableGameComponent: IGameComponent
    {
        public void Initialize(SpriteBatch spriteBatch);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
