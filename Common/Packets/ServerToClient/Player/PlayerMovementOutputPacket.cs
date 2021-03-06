using System;
using System.Numerics;
using Common.Definitions;
using Common.Network.IO;
using Common.Model.Shared;

namespace Common.Packets.ServerToClient.Player
{
    public class PlayerMovementOutputPacket: IPacket
    {
        public PacketType Id => PacketType.PLAYER_MOVEMENT_OUTPUT;

        public string PlayerId { get; set; }
        public Vector3 Position { get; set; }
        public Direction MovementType { get; set; }
        public bool IsMoving { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            PlayerId = packetReader.ReadString();
            Position = new Vector3(
                packetReader.ReadFloat(),
                packetReader.ReadFloat(),
                packetReader.ReadFloat());
            MovementType = (Direction)packetReader.ReadInteger();
            IsMoving = packetReader.ReadBool();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteString(PlayerId);
            packetWriter.WriteFloat(Position.X);
            packetWriter.WriteFloat(Position.Y);
            packetWriter.WriteFloat(Position.Z);
            packetWriter.WriteInteger((int)MovementType);
            packetWriter.WriteBool(IsMoving);
            return packetWriter.ToBytes();
        }
    }
}
