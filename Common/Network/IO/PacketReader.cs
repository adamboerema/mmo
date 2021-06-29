using System;
using System.IO;
using System.Text;

namespace Common.Network.IO
{
    public class PacketReader: IDisposable
    {
        private BinaryReader binaryReader;
        private MemoryStream memoryStream;

        public PacketReader(byte[] bytes)
        {
            memoryStream = new MemoryStream(bytes);
            binaryReader = new BinaryReader(memoryStream, Encoding.ASCII);
        }

        public int ReadInteger() => binaryReader.ReadInt32();
        public float ReadFloat() => binaryReader.ReadSingle();
        public short ReadShort() => binaryReader.ReadInt16();
        public bool ReadBool() => binaryReader.ReadBoolean();
        public byte ReadByte() => binaryReader.ReadByte();
        public byte[] ReadBytes(int count) => binaryReader.ReadBytes(count);
        public string ReadString() => binaryReader.ReadString();

        public void Dispose()
        {
            binaryReader.Dispose();
            memoryStream.Dispose();
        }
    }
}
