using System;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public enum RageAudioMetadataFileType
    {
        Dat4_AudioConfig = 4,
        Dat54_DataEntries = 54
    }

    public class RageAudioMetadata
    {    
        public IOFileReader Reader { get; }

        public RageAudioMetadataFileType Type { get; }

        public string[] StringTable { get; private set; }

        public byte[] DataSection;

        public RageAudioMetadata(IOFileReader reader)
        {
            Reader = reader;

            Type = (RageAudioMetadataFileType)Reader.ReadInt32(); //0x0-0x4

            if (!Enum.IsDefined(typeof(RageAudioMetadataFileType), Type))
                throw new FileFormatException("[RageAudioMetadata] Invalid file type: " + Type);

            var toRead = Reader.ReadInt32(); //0x4-0x8

            DataSection = Reader.ReadBytes(toRead);

            var stringSectionSize = Reader.ReadInt32(); // size of entire string table section indexes + strings

            stringSectionSize -= 4;

            var stringTableSize = Reader.ReadInt32(); // strings in string table

            StringTable = ReadStringSection(stringSectionSize, stringTableSize);
        }

        private string[] ReadStringSection(int dataSize, int stringCount)
        {
            var stringDataEnd = Reader.BaseStream.Position + dataSize;

            var indicesSize = 4 * stringCount; // size of string indices section              

            long basePos = Reader.BaseStream.Position + indicesSize;

            string[] result = new string[stringCount];

            for (int i = 0; i < stringCount; i++) // read string table
            {
                long currentPos = Reader.BaseStream.Position;

                var strOffset = Reader.ReadInt32();

                Reader.BaseStream.Seek(basePos + strOffset, SeekOrigin.Begin);

                result[i] = Reader.ReadString();

                Reader.BaseStream.Seek(currentPos, SeekOrigin.Begin);
            }

            Reader.BaseStream.Seek(stringDataEnd, SeekOrigin.Begin);

            return result;
        }
    }
}
