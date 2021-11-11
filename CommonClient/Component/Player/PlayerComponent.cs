using System;
using System.Numerics;
using Common.Component;
using Common.Model.Character;
using Common.Model.Shared;
using Common.Model.World;
using CommonClient.Network.Dispatch;

namespace CommonClient.Component.Player
{
    public class PlayerComponent: IComponent
    {
        public string Id { get; init; }
        public Vector3 Coordinates => _movement.Coordinates;

        private CharacterModel _charater;
        private MovementModel _movement;

        private IPlayerDispatch _playerDispatch;

        public PlayerComponent(
            PlayerConfiguration playerConfiguration,
            IPlayerDispatch playerDispatch)
        {
        }

        public void Update(GameTick gameTick, World world)
        {
            // Update here
        }
    }
}
