using System;
namespace Common.Network.Exceptions
{
    public class PacketDefinitionException: Exception
    {
        public PacketDefinitionException(int packetId, Exception exception):
            base(FormatMessage(packetId), exception)
        {
        }

        private static string FormatMessage(int packetId)
        {
            return $"Packet ID { packetId } is not valid"; 
        }
    }
}
