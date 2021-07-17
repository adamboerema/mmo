using System;
using Common.Model;

namespace Common.Extensions
{
    public static class PlayerExtensions
    {
        /// <summary>
        /// Move Coordinates of player
        /// </summary>
        /// <param name="model">Player model</param>
        /// <param name="speed">Speed of the movement</param>
        /// <returns></returns>
        public static PlayerModel MoveCoordinates(this PlayerModel model, float speed)
        {
            var character = model.Character;
            var coordinates = character.Coordinates;
            switch (character.MovementType)
            {
                case MovementType.UP:
                    coordinates.Y -= speed;
                    break;
                case MovementType.LEFT:
                    coordinates.X -= speed;
                    break;
                case MovementType.RIGHT:
                    coordinates.X += speed;
                    break;
                case MovementType.DOWN:
                    coordinates.Y += speed;
                    break;
                case MovementType.UP_LEFT:
                    coordinates.X -= speed;
                    coordinates.Y -= speed;
                    break;
                case MovementType.UP_RIGHT:
                    coordinates.X += speed;
                    coordinates.Y -= speed;
                    break;
                case MovementType.DOWN_LEFT:
                    coordinates.X -= speed;
                    coordinates.Y += speed;
                    break;
                case MovementType.DOWN_RIGHT:
                    coordinates.X += speed;
                    coordinates.Y += speed;
                    break;
                case MovementType.STOPPED:
                    break;
            }
            model.Character.Coordinates = coordinates;
            return model;
        }
    }
}
