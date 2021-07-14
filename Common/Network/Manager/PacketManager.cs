using System;
using System.Collections.Generic;
using Common.Definitions;
using Common.Network.IO;
using Common.Network.Parser;

namespace Common.Network.Manager
{
    public class PacketManager: IPacketManager
    {
        private const int HEADER_BYTE_LENGTH = 4;
        private IPacketParser packetParser;

        public PacketManager(IPacketDefinitions packetDefinitions)
        {
            packetParser = new PacketParser(packetDefinitions);
        }

        public IEnumerable<IPacket> Receive(byte[] bytes)
        {
            var packets = new List<IPacket>();
            using (var reader = new PacketReader(bytes))
            {
                var pointerPosition = 0;
                while(pointerPosition < bytes.Length)
                {
                    var packetLength = reader.ReadInteger();
                    var packetBytes = reader.ReadBytes(packetLength);
                    var packet = packetParser.ReadPacket(packetBytes);
                    pointerPosition += packetBytes.Length + HEADER_BYTE_LENGTH;

                    packets.Add(packet);

                    Console.WriteLine($"Byte Length: { bytes.Length }" +
                        $" Pointer Position: { pointerPosition }" +
                        $" Packet Id: {packet.Id}" +
                        $" Packet {packet}");
                }
            }
            return packets;
        }

        public byte[] Write(IPacket packet)
        {
            using(var outputWriter = new PacketWriter())
            using(var packetWriter = new PacketWriter())
            {
                var packetBytes = packetParser.WritePacket(packet, packetWriter);

                // Write packet header
                outputWriter.WriteInteger(packetBytes.Length);
                outputWriter.WriteBytes(packetBytes);
                return outputWriter.ToBytes();
            }
        }
    }
}
