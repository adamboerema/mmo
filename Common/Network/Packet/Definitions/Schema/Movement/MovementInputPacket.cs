﻿using System;
using Common.Network.Packet.IO;

namespace Common.Network.Packet.Definitions.Schema.Movement
{

    public class MovementInputPacket: IPacket
    {
        public PacketType Id => PacketType.MOVEMENT_INPUT;

        public MovementType Direction { get; set; }

        public IPacket ReadData(PacketReader packetReader)
        {
            Direction = (MovementType)packetReader.ReadInteger();
            return this;
        }

        public byte[] WriteData(PacketWriter packetWriter)
        {
            packetWriter.WriteInteger((int)Id);
            packetWriter.WriteInteger((int)Direction);
            return packetWriter.ToBytes();
        }
    }
}
