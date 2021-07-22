﻿using System;
using System.Numerics;
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
        /// <param name="maxWidth">Max world width</param>
        /// <param name="maxHeight">Max world height</param>
        /// <returns></returns>
        public static PlayerModel MoveCoordinates(
            this PlayerModel model,
            float speed,
            int maxWidth,
            int maxHeight)
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
            model.Character.Coordinates = ClampCoordinates(coordinates, maxWidth, maxHeight);
            return model;
        }


        /// <summary>
        /// Clamps the coordinates to the world
        /// </summary>
        /// <param name="coordinates"></param>
        /// <returns></returns>
        private static Vector3 ClampCoordinates(Vector3 coordinates, int maxWidth, int maxHeight)
        {
            coordinates.X = Math.Clamp(coordinates.X, 0, maxWidth);
            coordinates.Y = Math.Clamp(coordinates.Y, 0, maxHeight);
            return coordinates;
        }
    }
}