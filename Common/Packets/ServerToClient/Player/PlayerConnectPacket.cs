using System;
using System.Numerics;
using Common.Definitions;
using Common.Base;
using Common.Network.IO;

namespace Common.Packets.ServerToClient.Player
{
    public class PlayerConnectPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_CONNECTED;

        public string PlayerId { get; set; }
        public bool IsClient { get; set; }
        public bool IsMoving { get; set; }
        public Vector3 Position { get; set; }
        public Direction MovementType { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            IsClient = packetReader.ReadBool();
            IsMoving = packetReader.ReadBool();
            Position = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            MovementType = (Direction)packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(PlayerId);
            packetWriter.WriteBool(IsClient);
            packetWriter.WriteBool(IsMoving);
            packetWriter.WriteFloat(Position.X);
            packetWriter.WriteFloat(Position.Y);
            packetWriter.WriteFloat(Position.Z);
            packetWriter.WriteInteger((int)MovementType);
            return packetWriter.ToBytes();
        }
    }
}
