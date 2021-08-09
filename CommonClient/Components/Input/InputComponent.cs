using System;
using Common.Model;
using CommonClient.Engine.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CommonClient.Components.Movement
{
    public class InputComponent: GameComponent
    {
        private readonly IMovementManager _movementManager;
        private MovementType _movementType = MovementType.STOPPED;


        public InputComponent(Game game): base(game)
        {
            _movementManager = GameServices.GetService<IMovementManager>();
        }

        public override void Update(GameTime gameTime)
        {
            var currentMovementType = GetmovementType();
            if(_movementType != currentMovementType)
            {
                Console.WriteLine($"Changing to movement type: {currentMovementType}");
                _movementType = currentMovementType;
                _movementManager.UpdateClientMovementInput(currentMovementType);
            }
            base.Update(gameTime);
        }


        /// <summary>
        /// Get the type of movement based on keys
        /// </summary>
        /// <returns></returns>
        private MovementType GetmovementType()
        {
            var keyState = Keyboard.GetState();

            if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.A))
            {
                return MovementType.UP_LEFT;
            }
            else if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.D))
            {
                return MovementType.UP_RIGHT;
            }
            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.A))
            {
                return MovementType.DOWN_LEFT;
            }
            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.D))
            {
                return MovementType.DOWN_RIGHT;
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                return MovementType.UP;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                return MovementType.DOWN;
            }
            else if (keyState.IsKeyDown(Keys.A))
            {
                return MovementType.LEFT;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                return MovementType.RIGHT;
            }
            else
            {
                return MovementType.STOPPED;
            }
        }
    }
}
