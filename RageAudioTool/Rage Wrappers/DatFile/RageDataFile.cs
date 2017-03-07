using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public enum RageAudioMetadataFileType
    {
        Dat4 = 4,
        Dat10_ModularSynth = 10,
        Dat54_DataEntries = 54,
        Dat15_DynamicMixer,
        Dat16_Curves = 16,
        Dat22_Categories = 22,
        Dat151_Parameters = 151
    }

    public class RageDataFileReadReference : IOFileReader
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

    public class RageDataFileWriteReference : IOFileWriter
    {
        public RageDataFile FileObject { get; set; }
        public string Path { get; private set; }

        public RageDataFileWriteReference(string path) : 
            base(File.Open(path, FileMode.Open), Encoding.GetEncoding(1252))
        {   
            FileObject = null;
            Path = path;
        }
    }

    public class RageDataFile
    {    
        public RageAudioMetadataFileType Type { get; private set; }

        public string[] StringTable { get; set; }

        public byte[] DataSection { get; private set; }

        public bool NametablePresent { get; private set; }

        public int StringSectionSize { get; private set; }

        public Dictionary<uint, string> NametableObjects = new Dictionary<uint, string>();

        public RageDataFile()
        { }

        public virtual void Read(RageDataFileReadReference file)
        {
            Type = (RageAudioMetadataFileType) file.ReadInt32();

            if (!Enum.IsDefined(typeof(RageAudioMetadataFileType), Type))
                throw new FileFormatException("[RageAudioMetadata] Invalid file type: " + Type);

            NametablePresent = File.Exists(file.Path + ".nametable");

            if (NametablePresent)
            {
                using (var reader = new BinaryReader(File.Open(file.Path + ".nametable", FileMode.Open)))
                {
                    char result;

                    string text = string.Empty;

                    while (true)
                    {
                        if (reader.BaseStream.Position >= reader.BaseStream.Length)
                            break;

                        text = string.Empty;

                        while ((result = reader.ReadChar()) != '\0')
                        {
                            text += char.ToLower(result);
                        }

                        NametableObjects.Add(text.HashKey(), text);
                    }
                }
            }

            var toRead = file.ReadInt32(); //0x4-0x8

            DataSection = file.ReadBytes(toRead);

            StringSectionSize = file.ReadInt32(); // size of entire string table section indexes + strings

            StringSectionSize -= 4;

            var stringTableSize = file.ReadInt32(); // strings in string table

            StringTable = ReadStringSection(file, StringSectionSize, stringTableSize);

            file.FileObject = this;
        }

        public virtual void Write(RageDataFileWriteReference file)
        {
            file.Write((int)Type);
            file.Write(DataSection.Length);
            file.Write(DataSection);
            file.Write(StringSectionSize + 4);
            file.Write(StringTable.Length);
        }

        private string[] ReadStringSection(RageDataFileReadReference file, int dataSize, int stringCount)
        {
            var stringDataEnd = file.BaseStream.Position + dataSize;

            var indicesSize = 4 * stringCount; // size of string indices section              

            long basePos = file.BaseStream.Position + indicesSize;

            string[] result = new string[stringCount];

            for (int i = 0; i < stringCount; i++) // read string table
            {      
                var strOffset = file.ReadInt32();

                long currentPos = file.BaseStream.Position;

                file.BaseStream.Seek(basePos + strOffset, SeekOrigin.Begin);

                result[i] = file.ReadANSI();

                file.BaseStream.Seek(currentPos, SeekOrigin.Begin);
            }

            file.BaseStream.Seek(stringDataEnd, SeekOrigin.Begin);

            return result;
        }   
    }
}
