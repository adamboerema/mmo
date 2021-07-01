using System;
using Common.Network.Definitions;
using Common.Network.IO;

namespace Common.Network.Packets.Auth
{
    public class LoginResponsePacket: IPacket
    {
        public PacketType Id => PacketType.LOGIN_RESPONSE;
        public bool Success { get; set; }
        public string UserId { get; set; }


        public IPacket ReadData(PacketReader packetReader)
        {
            Success = packetReader.ReadBool();
            UserId = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteBool(Success);
            packetWriter.WriteString(UserId);
            return packetWriter.ToBytes();
        }
    }
}
