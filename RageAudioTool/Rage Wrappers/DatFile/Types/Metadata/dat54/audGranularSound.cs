using System;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile.Types;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audGranularSound : audSoundBase
    {
        public int WaveSlotIndex { get; set; } //0x0-0x4

        public audWaveFile WaveA { get; set; }

        public audWaveFile WaveB { get; set; }

        public audWaveFile WaveC { get; set; }

        public audWaveFile WaveD { get; set; }

        public audWaveFile WaveE { get; set; }

        public audWaveFile WaveF { get; set; }     

        public audGranularSoundData DataItem1 { get; set; } //0x34-0x3C

        public audGranularSoundData DataItem2 { get; set; } //0x3C-0x44

        public audGranularSoundData DataItem3 { get; set; } //0x44-0x4C

        public audGranularSoundData DataItem4 { get; set; } //0x4C-0x54

        public audGranularSoundData DataItem5 { get; set; } //0x54-0x5C

        public audGranularSoundData DataItem6 { get; set; } //0x5C-0x64

        public int UnkInt6 { get; set; } //0x64-0x68

        public int UnkInt7 { get; set; } //0x68-0x6C

        public ushort UnkShort { get; set; } //0x6C-0x6E

        public ushort UnkShort1 { get; set; } //0x6E-0x70

        public ushort UnkShort2 { get; set; } //0x70-0x72

        public ushort UnkShort3 { get; set; } //0x72-0x74

        public ushort UnkShort4 { get; set; } //0x74-0x76

        public ushort UnkShort5 { get; set; } //0x76-0x78

        //0x78-0x7C wave slot hash

    //    public byte UnkCount { get; set; } //0x7C-0x7D

        public Pair<float, float>[] UnkFloatData { get; set; } //0x7D-...

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(WaveSlotIndex);

                    writer.Write(WaveA.ContainerName.HashKey);
                    writer.Write(WaveA.FileName.HashKey);

                    writer.Write(WaveB.ContainerName.HashKey);
                    writer.Write(WaveB.FileName.HashKey);

                    writer.Write(WaveC.ContainerName.HashKey);
                    writer.Write(WaveC.FileName.HashKey);

                    writer.Write(WaveD.ContainerName.HashKey);
                    writer.Write(WaveD.FileName.HashKey);

                    writer.Write(WaveE.ContainerName.HashKey);
                    writer.Write(WaveE.FileName.HashKey);

                    writer.Write(WaveF.ContainerName.HashKey);
                    writer.Write(WaveF.FileName.HashKey);

                    writer.Write(DataItem1.UnkFlags);
                    writer.Write(DataItem1.UnkFlags1);
                    writer.Write(DataItem1.UnkByte);
                    writer.Write(DataItem1.UnkByte1);
                    writer.Write(DataItem1.UnkFloat);

                    writer.Write(DataItem2.UnkFlags);
                    writer.Write(DataItem2.UnkFlags1);
                    writer.Write(DataItem2.UnkByte);
                    writer.Write(DataItem2.UnkByte1);
                    writer.Write(DataItem2.UnkFloat);

                    writer.Write(DataItem3.UnkFlags);
                    writer.Write(DataItem3.UnkFlags1);
                    writer.Write(DataItem3.UnkByte);
                    writer.Write(DataItem3.UnkByte1);
                    writer.Write(DataItem3.UnkFloat);

                    writer.Write(DataItem4.UnkFlags);
                    writer.Write(DataItem4.UnkFlags1);
                    writer.Write(DataItem4.UnkByte);
                    writer.Write(DataItem4.UnkByte1);
                    writer.Write(DataItem4.UnkFloat);

                    writer.Write(DataItem5.UnkFlags);
                    writer.Write(DataItem5.UnkFlags1);
                    writer.Write(DataItem5.UnkByte);
                    writer.Write(DataItem5.UnkByte1);
                    writer.Write(DataItem5.UnkFloat);

                    writer.Write(DataItem6.UnkFlags);
                    writer.Write(DataItem6.UnkFlags1);
                    writer.Write(DataItem6.UnkByte);
                    writer.Write(DataItem6.UnkByte1);
                    writer.Write(DataItem6.UnkFloat);

                    writer.Write(UnkInt6);

                    writer.Write(UnkInt7);

                    writer.Write(UnkShort);

                    writer.Write(UnkShort1);

                    writer.Write(UnkShort2);

                    writer.Write(UnkShort3);

                    writer.Write(UnkShort4);

                    writer.Write(UnkShort5);

                    writer.Write(AudioTracks[0].HashKey);

                    writer.Write(UnkFloatData.Length);

                    for (int i = 0; i < UnkFloatData.Length; i++)
                    {
                        writer.Write(UnkFloatData[i].First);

                        writer.Write(UnkFloatData[i].Second);
                    }
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                WaveSlotIndex = reader.ReadInt32();

                WaveA = new audWaveFile(
                    new audHashString(parent, reader.ReadUInt32()), 
                    new audHashString(parent, reader.ReadUInt32()));

                WaveB = new audWaveFile(
                    new audHashString(parent, reader.ReadUInt32()),
                    new audHashString(parent, reader.ReadUInt32()));

                WaveC = new audWaveFile(
                    new audHashString(parent, reader.ReadUInt32()),
                    new audHashString(parent, reader.ReadUInt32()));

                WaveD = new audWaveFile(
                    new audHashString(parent, reader.ReadUInt32()),
                    new audHashString(parent, reader.ReadUInt32()));

                WaveE = new audWaveFile(
                    new audHashString(parent, reader.ReadUInt32()),
                    new audHashString(parent, reader.ReadUInt32()));

                WaveF = new audWaveFile(
                    new audHashString(parent, reader.ReadUInt32()),
                    new audHashString(parent, reader.ReadUInt32()));

                DataItem1 = new audGranularSoundData(reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadSingle());

                DataItem2 = new audGranularSoundData(reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadSingle());

                DataItem3 = new audGranularSoundData(reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadSingle());

                DataItem4 = new audGranularSoundData(reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadSingle());

                DataItem5 = new audGranularSoundData(reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadSingle());

                DataItem6 = new audGranularSoundData(reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadByte(),
                    reader.ReadSingle());

                UnkInt6 = reader.ReadInt32();

                UnkInt7 = reader.ReadInt32();

                UnkShort = reader.ReadUInt16();

                UnkShort1 = reader.ReadUInt16();

                UnkShort2 = reader.ReadUInt16();

                UnkShort3 = reader.ReadUInt16();

                UnkShort4 = reader.ReadUInt16();

                UnkShort5 = reader.ReadUInt16();

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                var itemCount = reader.ReadByte();

                UnkFloatData = new Pair<float, float>[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
                    UnkFloatData[i] = new Pair<float, float>(reader.ReadSingle(),
                        reader.ReadSingle());
                }

                return data.Length;
            }
        }

        public override string ToString()
        {
            return "";// BitConverter.ToString(Data).Replace("-", "");
        }

        public audGranularSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audGranularSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audGranularSound()
        { }
    }

    [TypeConverter(typeof(NamedObjectConverter))]
    public class audGranularSoundData
    {
        public audGranularSoundData(byte unkFlags, byte unkFlags1, byte unkByte, byte unkByte1, float unkFloat)
        {
            UnkFlags = unkFlags;
            UnkFlags1 = unkFlags1;
            UnkByte = unkByte;
            UnkByte1 = unkByte1;
            UnkFloat = unkFloat;
        }

        public byte UnkFlags { get; set; } //0x0-0x1

        public byte UnkFlags1 { get; set; } //0x1-0x2

        public byte UnkByte { get; set; } //0x2-0x3

        public byte UnkByte1 { get; set; } //0x3-0x4

        public float UnkFloat { get; set; } //0x4-0x8
    }

    [TypeConverter(typeof(NamedObjectConverter))]
    public class audWaveFile
    {
        public audWaveFile(audHashString waveContainer, audHashString fileName)
        {
            ContainerName = waveContainer;
            FileName = fileName;
        }

        public audHashString ContainerName { get; set; } //0x0-0x4
        public audHashString FileName { get; set; } //0x4-0x8
    }
}
