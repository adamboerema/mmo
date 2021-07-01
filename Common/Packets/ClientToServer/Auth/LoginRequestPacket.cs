using System;
using Common.Network.Definitions;
using Common.Network.IO;

namespace Common.Network.Packets.Auth
{
    public class LoginRequestPacket: IPacket
    {
        public PacketType Id => PacketType.LOGIN_REQUEST;
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
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(Username);
            packetWriter.WriteString(Password);
            return packetWriter.ToBytes();
        }
    }
}
