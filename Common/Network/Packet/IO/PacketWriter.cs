using System;
using System.IO;
using System.Text;

namespace Common.Network.Packet.IO
{
    public class PacketWriter: IDisposable
    {
        private BinaryWriter binaryWriter;
        private MemoryStream memoryStream;

        public PacketWriter(int capacity)
        {
            memoryStream = new MemoryStream(capacity);
            binaryWriter = new BinaryWriter(memoryStream, Encoding.ASCII);
        }

        public void WriteInteger(int value) => binaryWriter.Write(value);
        public void WriteFloat(float value) => binaryWriter.Write(value);
        public void WriteShort(short value) => binaryWriter.Write(value);
        public void WriteByte(byte value) => binaryWriter.Write(value);
        public void WriteBytes(byte[] value) => binaryWriter.Write(value);
        
        public void WriteString(string value)
        {
            Encoding.ASCII.GetBytes(value);
            binaryWriter.Write(value);
        }

        public byte[] ToArray()
        {
            return memoryStream.ToArray();
        }

        public void Dispose()
        {
            binaryWriter.Dispose();
            memoryStream.Dispose();
        }
    }
}
