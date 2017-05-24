using System;
using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audModularSynthSound : audSoundBase
    {      
        public audHashString UnkHash { get; set; } //0x0-0x4

        public audHashString UnkHash1 { get; set; } //0x4-0x8

        public float UnkFloat { get; set; } //0x8-0xC

        public int UnkInt2 { get; set; } //0xC-0x10

        public audModularSynthSoundData[] UnkArrayData { get; set; } //0x28-..

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(UnkHash.HashKey);

                    writer.Write(UnkHash1.HashKey);

                    writer.Write(UnkFloat);

                    writer.Write(UnkInt2);

                    writer.Write(AudioTracks.Count);

                    for (int i = 0; i < AudioTracks.Count; i++)
                    {
                        writer.Write(AudioTracks[i].HashKey);
                    }

                    writer.Write(Enumerable.Repeat<byte>(0x0, (4 - AudioTracks.Count) << 2).ToArray());

                    writer.Write(UnkArrayData.Length);

                    for (int i = 0; i < UnkArrayData.Length; i++)
                    {
                        writer.Write(UnkArrayData[i].UnkHash.HashKey);

                        writer.Write(UnkArrayData[i].ParameterHash.HashKey);

                        writer.Write(UnkArrayData[i].Value);
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
                UnkHash = new audHashString(parent, reader.ReadUInt32());

                UnkHash1 = new audHashString(parent, reader.ReadUInt32());

                UnkFloat = reader.ReadSingle();

                UnkInt2 = reader.ReadInt32();

                var trackCount = reader.ReadInt32();

                for (int i = 0; i < trackCount; i++)
                {
                    AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));
                }

                reader.ReadBytes((4 - trackCount) << 2); //0x14-0x24

                var itemCount = reader.ReadInt32();

                UnkArrayData = new audModularSynthSoundData[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
                    UnkArrayData[i] = new audModularSynthSoundData(
                        new audHashString(parent, reader.ReadUInt32()), 
                        new audHashString(parent, reader.ReadUInt32()), 
                        reader.ReadSingle());
                }

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";// BitConverter.ToString(Data).Replace("-", "");
        }

        public audModularSynthSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audModularSynthSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audModularSynthSound()
        { }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class audModularSynthSoundData
    {
        public audHashString UnkHash { get; set; }

        public audHashString ParameterHash { get; set; }

        public float Value { get; set; }

        public audModularSynthSoundData(audHashString unkHash, audHashString parameterHash, float value)
        {
            UnkHash = unkHash;
            ParameterHash = parameterHash;
            Value = value;
        }
    }
}
