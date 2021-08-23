using System;
using System.Numerics;
using Common.Model.Base;

namespace Common.Base
{
    public class PlayerModel: BaseCharacterModel
    {

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
