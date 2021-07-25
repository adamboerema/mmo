using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CommonClient.Components
{
    public interface IGameComponent
    {
        public void Update(GameTime gameTime);
    }
}
