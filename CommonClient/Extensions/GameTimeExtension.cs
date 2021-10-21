using System;
using Common.Model.Shared;
using Microsoft.Xna.Framework;

namespace CommonClient.Extensions
{
    public static class GameTimeExtension
    {

        public static GameTick ToGameTick(this GameTime gameTime)
        {
            return new GameTick(
                gameTime.ElapsedGameTime.Milliseconds,
                gameTime.TotalGameTime.Milliseconds);
        }
    }
}
