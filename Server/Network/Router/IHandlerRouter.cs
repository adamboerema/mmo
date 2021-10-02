using Common.Definitions;

namespace Server.Network.Router
{
    public interface IHandlerRouter
    {
        public void Route(string connectionId, IPacket packet);
    }
}
