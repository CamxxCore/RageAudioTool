using System.IO;
using System.Text;

namespace RageAudioTool.IO
{
    public class IoFileReader : BinaryReader
    {
        public IoFileReader(Stream input) : base(input)
        { }

        public IoFileReader(Stream input, Encoding encoding) : base(input, encoding)
        { }

        public IoFileReader(byte[] input) : base(new MemoryStream(input))
        { }

        public IoFileReader(byte[] input, Encoding encoding) : base(new MemoryStream(input), encoding)
        { }

        public string ReadString(int length)
        {
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += ReadChar();
            }

            return result;
        }

        public static implicit operator Stream(IoFileReader reader)
        {
            return reader.BaseStream;
        }

        public string ReadAnsi()
        {
            char c;
            string result = "";

            while ((c = ReadChar()) != '\0')
            {
                result += c;
            }

            return result;
        }
    }
}
