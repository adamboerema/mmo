using System;
using System.Numerics;
using Common.Model;
using Common.Model.Character;

namespace Common.Base
{
    public class PlayerModel
    {
        public string Id { get; init; }

        public CharacterModel Character { get; init; }

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
