using System;
namespace Common.Network.Packet.Definitions
{
    public interface IPacketDefinition
    {
        void ParseData(byte[] data);
    }
}
