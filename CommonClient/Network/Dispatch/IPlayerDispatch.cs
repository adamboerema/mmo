using System;
using Common.Model.Shared;

namespace CommonClient.Network.Dispatch
{
    public interface IPlayerDispatch
    {
        public void DispatchPlayerMovement(
            Direction direction,
            bool isMoving);
    }
}
