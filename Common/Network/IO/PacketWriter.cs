using System;
using System.IO;
using System.Text;

namespace Common.Network.IO
{
    public class PacketWriter: IDisposable
    {
        private BinaryWriter binaryWriter;
        private MemoryStream memoryStream;
        private string NULL = "" + char.MinValue;


        public PacketWriter(int capacity = 0)
        {
            memoryStream = new MemoryStream(capacity);
            binaryWriter = new BinaryWriter(memoryStream, Encoding.ASCII);
        }

        public void WriteInteger(int value) => binaryWriter.Write(value);
        public void WriteFloat(float value) => binaryWriter.Write(value);
        public void WriteShort(short value) => binaryWriter.Write(value);
        public void WriteBool(bool value) => binaryWriter.Write(value);
        public void WriteByte(byte value) => binaryWriter.Write(value);
        public void WriteBytes(byte[] value) => binaryWriter.Write(value);
        
        public void WriteString(string value)
        {
            Encoding.ASCII.GetBytes(value);
            binaryWriter.Write(value);
        }

        public void WriteNullableString(string value)
        {
            var prepared = value ?? NULL;
            WriteString(prepared);
        }

        public byte[] ToBytes()
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
