using System;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;

namespace CommonClient.Components.Player
{
    public class PlayerDrawableComponent: DrawableGameComponent
    {
        private readonly IPlayerManager _playerManager;

        public PlayerDrawableComponent(Game game): base(game)
        {
            _playerManager = GameServices.GetService<IPlayerManager>();
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
