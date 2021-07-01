using Common.Definitions;

namespace Server.Network.Handler.Factory
{
    public interface IHandlerRouter
    {
        public void Route(string connectionId, IPacket packet);
    }
}
