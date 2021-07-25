using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components
{
    public interface IDrawableGameComponent: IGameComponent
    {
        public void Initialize(SpriteBatch spriteBatch);

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
    }
}
