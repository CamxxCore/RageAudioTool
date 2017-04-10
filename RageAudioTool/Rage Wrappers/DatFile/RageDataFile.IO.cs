using System.IO;
using System.Text;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public sealed class RageDataFileReadReference : IOFileReader
    {
        public RageDataFile FileObject { get; set; }

        public string Path { get; private set; }

        public RageDataFileReadReference(string path) : 
            base(File.Open(path, FileMode.Open), Encoding.GetEncoding(1252))
        {
            FileObject = null;
            Path = path;
        }
    }

    public sealed class RageDataFileWriteReference : IOBinaryWriter
    {
        public RageDataFile FileObject { get; set; }

        public string Path { get; private set; }

        public RageDataFileWriteReference(string path) : 
            base(File.Open(path, FileMode.OpenOrCreate), Encoding.GetEncoding(1252))
        {
            FileObject = null;
            Path = path;
        }
    }
}
