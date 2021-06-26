﻿using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Schema.Movement
{
    public class MovementOutputPacket: IPacket
    {
        public int Id => Definitions.MOVEMENT_OUTPUT;

        public string PlayerId { get; set; }

        public int X { get; set; }
        public int Y { get; set; }
        public int Z { get; set; }
        public MovementType MovementType { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            X = packetReader.ReadInteger();
            Y = packetReader.ReadInteger();
            Z = packetReader.ReadInteger();
            MovementType = (MovementType)packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger(X);
            packetWriter.WriteInteger(Y);
            packetWriter.WriteInteger(Z);
            packetWriter.WriteInteger((int)MovementType);
            return packetWriter.ToBytes();
        }
    }
}
