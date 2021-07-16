using System;
using Common.Model;

namespace Common.Utility
{
    public static class MovementUtility
    {
        public static PlayerModel MovePlayerCoordinates(float speed, PlayerModel model)
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
            return model;
        }
    }
}
