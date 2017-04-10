using System;
using System.IO;
using System.Text;

namespace RageAudioTool.IO
{
    public class IOBinaryWriter : BinaryWriter
    {
        public IOBinaryWriter(Stream output) : base(output)
        { }

        public IOBinaryWriter(Stream output, Encoding encoding) : base(output, encoding)
        { }

        public void WriteFormat(string format, params object[] args)
        {
            Write(string.Format(format, args));
        }

        public void WriteLine(string value, int numTrailingLineSpaces)
        {
            if (numTrailingLineSpaces < 1)
                numTrailingLineSpaces = 1;
            value += Environment.NewLine;
            Write(value);
        }

        public void WriteLine()
        {
            WriteLine("", 1);
        }

        public void WriteLine(string value)
        {
            WriteLine(value, 1);
        }

        public void WriteANSI(string value)
        {
            Write(value.ToBytes());
            Write((byte)0);
        }
    }
}
