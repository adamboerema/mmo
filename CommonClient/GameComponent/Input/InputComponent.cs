using System;
using Common.Model.Shared;
using CommonClient.Engine.Player;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CommonClient.GameComponent.Input
{
    public class InputComponent: Microsoft.Xna.Framework.GameComponent
    {
        private readonly IPlayerManager _playerManager;
        private Direction _movementType = Direction.DOWN;
        private bool _isMoving = false;

        public InputComponent(Game game): base(game)
        {
            _playerManager = GameServices.GetService<IPlayerManager>();
        }

        public override void Update(GameTime gameTime)
        {
            var keyboardState = Keyboard.GetState();
            var isMoving = IsMovementKeyDown(keyboardState);
            var currentMovementType = GetDirection(keyboardState);
            if(_movementType != currentMovementType || _isMoving != isMoving)
            {
                Console.WriteLine($"Changing to movement type: {currentMovementType} and {isMoving}");
                _isMoving = isMoving;
                _movementType = currentMovementType;
                _playerManager.UpdateClientMovementInput(currentMovementType, isMoving);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Check if movement key is down
        /// </summary>
        /// <param name="keyState"></param>
        /// <returns></returns>
        private bool IsMovementKeyDown(KeyboardState keyState)
        {
            return keyState.IsKeyDown(Keys.W)
                || keyState.IsKeyDown(Keys.A)
                || keyState.IsKeyDown(Keys.S)
                || keyState.IsKeyDown(Keys.D);
        }


        /// <summary>
        /// Get the type of movement based on keys
        /// </summary>
        /// <returns></returns>
        private Direction GetDirection(KeyboardState keyState)
        {
            if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.A))
            {
                return Direction.UP_LEFT;
            }
            else if (keyState.IsKeyDown(Keys.W) && keyState.IsKeyDown(Keys.D))
            {
                return Direction.UP_RIGHT;
            }
            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.A))
            {
                return Direction.DOWN_LEFT;
            }
            else if (keyState.IsKeyDown(Keys.S) && keyState.IsKeyDown(Keys.D))
            {
                return Direction.DOWN_RIGHT;
            }
            else if (keyState.IsKeyDown(Keys.W))
            {
                return Direction.UP;
            }
            else if (keyState.IsKeyDown(Keys.S))
            {
                return Direction.DOWN;
            }
            else if (keyState.IsKeyDown(Keys.A))
            {
                return Direction.LEFT;
            }
            else if (keyState.IsKeyDown(Keys.D))
            {
                return Direction.RIGHT;
            }
            else
            {
                return _movementType;
            }
        }
    }
}
