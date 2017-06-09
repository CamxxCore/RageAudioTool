using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using RageAudioTool.IO;
using System.Windows.Forms;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public enum RageAudioMetadataFileType
    {
        Dat4 = 4,
        Dat10ModularSynth = 10,
        Dat54DataEntries = 54,
        Dat15DynamicMixer,
        Dat16Curves = 16,
        Dat22Categories = 22,
        Dat151Parameters = 151
    }

    public abstract class RageDataFile : IDisposable
    {    
        public RageAudioMetadataFileType Type { get; set; }

        public string[] StringTable { get; set; }

        public byte[] DataSection { get; private set; }

        public bool NametablePresent { get; private set; }

        public int StringSectionSize { get; private set; }

        public audDataBase[] DataItems { get; set; }

        public audHash[] WaveTracks { get; set; }

        public audHash[] WaveContainers { get; set; }

        public Dictionary<uint, string> Nametable =
            new Dictionary<uint, string>();

        public int UnkDataSectionValue { get; set; }

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

            UnkDataSectionValue = BitConverter.ToInt32(DataSection, 0); // what the hell is this? timestamp maybe?

            StringSectionSize = file.ReadInt32() - 4; // size of entire string table section indexes + strings

            var tableSize = file.ReadInt32(); // strings in string table

            StringTable = ReadStringSection(file, StringSectionSize, tableSize);

            var itemCount = file.ReadInt32();

            DataItems = ReadDataItems(file, itemCount);

          //  itemCount = file.ReadInt32();

          //  WaveTracks = ReadWaveTracks(file, itemCount);
        }

        public virtual audHash[] ReadWaveTracks(RageDataFileReadReference file, int itemCount)
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

        public virtual void Write(RageDataFileWriteReference file)
        {
            StringSectionSize = StringTable.Sum(str => str.Length + 5);       

            CreateDataSection();

            file.Write((int)Type);

            file.Write(DataSection.Length);

            file.Write(DataSection);

            file.Write(StringSectionSize + 4);

            file.Write(StringTable.Length);

            WriteStringSection(file);

            file.Write(DataItems.Length);

            WriteDataOffsets(file);

            WriteWaveTracks(file);

            WriteWaveContainers(file);
        }

        private void ReadNametableItems(string nametablePath)
        {
            using (var reader = new IOBinaryReader(File.Open(nametablePath, FileMode.Open)))
            {
                while (true)
                {
                    if (reader.BaseStream.Position >= reader.BaseStream.Length)
                        break;

                    string text = reader.ReadAnsi();

                    if (!Nametable.ContainsValue(text))
                    {
                        Nametable.Add(text.HashKey(), text);
                    }

                    else MessageBox.Show("Ignoring duplicate entry \"" + text + "\" in \"" + nametablePath);      
                }
            }
        }

        public abstract audDataBase[] ReadDataItems(RageDataFileReadReference file, int numItems);

        private void CreateDataSection()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(ms))
                {
                    writer.Write(UnkDataSectionValue);

                    for (int i = 0; i < DataItems.Length; i++)
                    {
                        var data = DataItems[i].Serialize();

                        DataItems[i].FileOffset =
                            (int)writer.BaseStream.Position;

                        writer.Write(data);
                    }
                }

                DataSection = ms.ToArray();
            }         
        }

        protected abstract void WriteDataOffsets(RageDataFileWriteReference file);

        protected void WriteWaveContainers(RageDataFileWriteReference file)
        {
            if (this is RageAudioMetadata5)
            {
                var items = DataItems
                    .OfType<audSoundBase>()
                    .SelectMany(x => x.AudioContainers.BaseList.Select(y => x.FileOffset + y.Offset)).ToArray();

                file.Write(items.Length);
     
                foreach (var offset in items)
                {
                    file.Write(offset + 8);
                }
            }

            else
            {
                file.Write(WaveContainers.Length);

                foreach (var item in WaveContainers)
                {
                    file.Write(item.FileOffset + 8);
                }
            }
        }

        protected void WriteWaveTracks(RageDataFileWriteReference file)
        {
            if (this is RageAudioMetadata5)
            {
                var items = DataItems
                    .OfType<audSoundBase>()
                    .SelectMany(x => x.AudioTracks.BaseList.Select(y => x.FileOffset + y.Offset)).ToArray();

                file.Write(items.Length);

                foreach (var offset in items)
                {
                    file.Write(offset + 8);
                }
            }

            else
            {
                file.Write(WaveTracks.Length);

                foreach (var item in WaveTracks)
                {
                    file.Write(item.FileOffset + 8);
                }
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

                result[i] = file.ReadAnsi();

                if (!Nametable.ContainsValue(result[i]))
                {
                    Nametable.Add(result[i].HashKey(), result[i]);
                }

                file.BaseStream.Seek(currentPos, SeekOrigin.Begin);
            }

            file.BaseStream.Seek(stringDataEnd, SeekOrigin.Begin);

            return result;
        }

        private void WriteStringSection(RageDataFileWriteReference file)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    for (int i = 0; i < StringTable.Length; i++)
                    {
                        file.Write((int)writer.BaseStream.Position); // write string offset

                        writer.WriteAnsi(StringTable[i]);
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

            if (WaveTracks != null)
            {
                Array.Clear(WaveTracks, 0, WaveTracks.Length);
            }

            if (WaveContainers != null)
            {
                Array.Clear(WaveContainers, 0, WaveContainers.Length);
            }

            StringSectionSize = 0;

            Nametable.Clear();
        }
    }
}
