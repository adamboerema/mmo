using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Schema.Auth
{
    public class LoginResponsePacket: IPacket
    {
        public int Id => Definitions.LOGIN_RESPONSE;
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
            packetWriter.WriteInteger(Id);
            packetWriter.WriteBool(Success);
            packetWriter.WriteString(UserId);
            return packetWriter.ToBytes();
        }
    }
}
