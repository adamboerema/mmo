using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Client
{
    public class LoginRequestPacket: IPacket
    {
        public int Header => ClientDefinitions.LOGIN_REQUEST;
        public string Username { get; set; }
        public string Password { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            Username = packetReader.ReadString();
            Password = packetReader.ReadString();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger(Id);
            packetWriter.WriteString(Username);
            packetWriter.WriteString(Password);
            return packetWriter.ToBytes();
        }
    }
}
