using System;
using System.Numerics;
using Common.Model;

namespace Common.Base
{
    public class PlayerModel
    {
        public string Id { get; init; }

        public CharacterModel Character { get; private set; }

        //public PlayerModel(
        //    CharacterModel character)
        //{
        //    Id = id;
        //    Name = name;
        //    Direction = direction;
        //    Coordinates = coordinates;
        //    IsMoving = isMoving;
        //    MovementSpeed = movementSpeed;
        //}

        public PlayerModel(
            string id,
            CharacterModel character)
        {
            Id = id;
            Character = character;
        }

        public PlayerModel UpdateCoordinates(
            Vector3 coordinates,
            Direction direction,
            bool isMoving)
        {
            Character.Coordinates = coordinates;
            Character.Direction = direction;
            Character.IsMoving = isMoving;
            return this;
        }

        public PlayerModel UpdateDirection(Direction direction, bool isMoving)
        {
            Character.Direction = direction;
            Character.IsMoving = isMoving;
            return this;
        }
    }
}
