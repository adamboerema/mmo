using System;
using CommonClient.Bus.Packet;

namespace CommonClient.Network.Dispatch
{
    public class EnemyDispatch: IEnemyDispatch
    {
        private IDispatchPacketBus _dispatchPacketBus;

        public EnemyDispatch(IDispatchPacketBus dispatchPacketBus)
        {
            _dispatchPacketBus = dispatchPacketBus;
        }
    }
}
