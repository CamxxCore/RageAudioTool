using System;
using System.IO;
using System.Linq;
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
            value += Enumerable.Repeat(Environment.NewLine, numTrailingLineSpaces);
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

        public void WriteAnsi(string value)
        {
            Write(value.ToBytes());
            Write((byte)0);
        }
    }
}
