using System;
using System.Numerics;
using Common.Model.Base;

namespace Common.Base
{
    public class PlayerModel: BaseCharacterModel
    {
        public PlayerModel(
            string id,
            string name,
            Direction direction,
            Vector3 coordinates,
            bool isMoving,
            float movementSpeed)
        {
            Id = id;
            Name = name;
            Direction = direction;
            Coordinates = coordinates;
            IsMoving = isMoving;
            MovementSpeed = movementSpeed;
        }

        public PlayerModel UpdateCoordinates(
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            Coordinates = coordinates;
            Direction = direction;
            IsMoving = isMoving;
            return this;
        }

        public PlayerModel UpdateDirection(Direction direction, bool isMoving)
        {
            Direction = direction;
            IsMoving = isMoving;
            return this;
        }
    }
}
