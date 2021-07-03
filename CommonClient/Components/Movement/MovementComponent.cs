using System;
using Common.Model;
using CommonClient.Bus.Packet;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CommonClient.Components.Movement
{
    public class MovementComponent: GameComponent
    {
        private readonly IDispatchPacketBus _dispatchPacketBus;
        private MovementType _movementType = MovementType.STOPPED;

        public MovementComponent(Game game): base(game)
        {
            _dispatchPacketBus = GameServices.GetService<IDispatchPacketBus>();
        }

        public override void Update(GameTime gameTime)
        {
            
            base.Update(gameTime);
        }

        private MovementType GetmovementType()
        {
            var keyPressed = Keyboard.GetState();
            return MovementType.STOPPED;
        }
    }
}
