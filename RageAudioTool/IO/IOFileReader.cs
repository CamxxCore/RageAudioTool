using System.Text;
using System.IO;

namespace RageAudioTool.IO
{
    public class IOFileReader : BinaryReader
    {
        public IOFileReader(Stream input) : base(input)
        { }

        public IOFileReader(Stream input, Encoding encoding) : base(input, encoding)
        { }

        public IOFileReader(byte[] input) : base(new MemoryStream(input))
        { }

        public IOFileReader(byte[] input, Encoding encoding) : base(new MemoryStream(input), encoding)
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

   /*     public override string ReadString()
        {
            char c;
            string result = "";

            while ((c = ReadChar()) != '\0')
            {
                result += c;
            }

            return result;
        }*/
    }
}
