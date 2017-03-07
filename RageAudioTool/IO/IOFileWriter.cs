using System;
using System.Text;
using System.IO;

namespace RageAudioTool.IO
{
    public class IOFileWriter : BinaryWriter
    {
        public IOFileWriter(Stream output, Encoding encoding) : base(output, encoding)
        { }

        public override void Write(string value)
        {
            base.Write(value.ToBytes());
        }

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
    }
}
