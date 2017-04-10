using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using RageAudioTool.IO;
using RageAudioTool.Types;

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

    public abstract class RageDataFile : IDisposable
    {    
        public RageAudioMetadataFileType Type { get; set; }

        public string[] StringTable { get; set; }

        public byte[] DataSection { get; private set; }

        public bool NametablePresent { get; private set; }

        public int StringSectionSize { get; private set; }

        public audDataBase[] DataItems { get; set; }

        public audHash[] HashItems { get; set; }

        public audHash[] HashItems1 { get; set; }

        public Dictionary<uint, string> Nametable =
            new Dictionary<uint, string>();

        public RageDataFile()
        { }

        public virtual void Read(RageDataFileReadReference file)
        {
            file.FileObject = this;

            Type = (RageAudioMetadataFileType) file.ReadInt32();

            if (!Enum.IsDefined(typeof(RageAudioMetadataFileType), Type))
                throw new FileFormatException("[RageAudioMetadata] Invalid file type: " + Type);

            string nametablePath = Path.ChangeExtension(file.Path, ".nametable");

            NametablePresent = File.Exists(nametablePath);

            if (NametablePresent) ReadNametableItems(nametablePath);

            var toRead = file.ReadInt32(); //0x4-0x8         

            DataSection = file.ReadBytes(toRead);

            StringSectionSize = file.ReadInt32() - 4; // size of entire string table section indexes + strings

            var tableSize = file.ReadInt32(); // strings in string table

            StringTable = ReadStringSection(file, StringSectionSize, tableSize);

            var itemCount = file.ReadInt32();

            DataItems = ReadDataItems(file, itemCount);

            itemCount = file.ReadInt32();

            HashItems = ReadHashItems(file, itemCount);

            itemCount = file.ReadInt32();

            HashItems1 = ReadHashItems1(file, itemCount);

            
        }

        public virtual void Write(RageDataFileWriteReference file)
        {
            CreateDataSection();

            file.Write((int)Type);

            file.Write(DataSection.Length);

            file.Write(DataSection);

            file.Write(StringSectionSize + 4);

            file.Write(StringTable.Length);

            WriteStringSection(file);

            file.Write(DataItems.Length);

            WriteDataOffsets(file);

            file.Write(HashItems.Length);

            WriteHashOffsets(file);

            file.Write(HashItems1.Length);

            WriteHashOffsets1(file);
        }

        private void ReadNametableItems(string nametablePath)
        {
            using (var reader = new BinaryReader(File.Open(nametablePath, FileMode.Open)))
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
                        text += result;
                    }

                    if (!Nametable.ContainsValue(text))
                    {
                        Nametable.Add(text.HashKey(), text);
                    }

                    else System.Windows.Forms.MessageBox.Show("Ignoring duplicate entry \"" + text + "\" in \"" + nametablePath);      
                }
            }
        }

        public abstract audDataBase[] ReadDataItems(RageDataFileReadReference file, int numItems);

        public virtual audHash[] ReadHashItems(RageDataFileReadReference file, int itemCount)
        {
            var items = new audHash[itemCount];

            byte[] buffer = new byte[0x4];

            for (int i = 0; i < itemCount; i++)
            {
                var offset = file.ReadInt32();

                offset -= 8;

                Buffer.BlockCopy(DataSection, offset, buffer, 0, 4);

                items[i] = new audHash(this);

                items[i].Deserialize(buffer);

                items[i].FileOffset = offset;

                items[i].Length = 4;
            }

            return items;
        }

        public virtual audHash[] ReadHashItems1(RageDataFileReadReference file, int itemCount)
        {
            var items = new audHash[itemCount];

            byte[] buffer = new byte[0x4];

            for (int i = 0; i < itemCount; i++)
            {
                var offset = file.ReadInt32();

                offset -= 8;

                Buffer.BlockCopy(DataSection, offset, buffer, 0, 4);

                items[i] = new audHash(this);

                items[i].Deserialize(buffer);

                items[i].FileOffset = offset;

                items[i].Length = 4;
            }

            return items;
        }

        private void CreateDataSection()
        {
            foreach (var item in DataItems)
            {
                var bytes = item.Serialize();

                Buffer.BlockCopy(bytes, 0, DataSection, item.FileOffset, bytes.Length);
            }           
            
         /*   var dataEntries = DataItems.Concat(HashItems).Concat(HashItems1);

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    foreach (audDataBase item in dataEntries)
                    {
                        item.FileOffset = (int)writer.BaseStream.Position;

                        writer.Write(item.Serialize());
                    }
                }

                DataSection = ms.ToArray();
            }*/
        }

        protected abstract void WriteDataOffsets(RageDataFileWriteReference file);

        protected void WriteHashOffsets(RageDataFileWriteReference file)
        {
            foreach (audDataBase item in HashItems)
            {
                file.Write(item.FileOffset + 8);
            }
        }

        protected void WriteHashOffsets1(RageDataFileWriteReference file)
        {
            foreach (audDataBase item in HashItems1)
            {
                file.Write(item.FileOffset + 8);
            }
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

        private void WriteStringSection(RageDataFileWriteReference file)
        {
            List<int> indices = new List<int>();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    for (int i = 0; i < StringTable.Length; i++)
                    {
                        file.Write((int)writer.BaseStream.Position); // write string offset

                        writer.WriteANSI(StringTable[i]);                  
                    }
                }

                file.Write(stream.ToArray()); // write string data
            }
        }

        public void Dispose()
        {
            if (DataSection != null)
            {
                Array.Clear(DataSection, 0, DataSection.Length);
            }

            if (DataItems != null)
            {
                Array.Clear(DataItems, 0, DataItems.Length);
            }

            if (HashItems != null)
            {
                Array.Clear(HashItems, 0, HashItems.Length);
            }

            if (HashItems1 != null)
            {
                Array.Clear(HashItems1, 0, HashItems1.Length);
            }

            StringSectionSize = 0;

            Nametable.Clear();
        }
    }
}
