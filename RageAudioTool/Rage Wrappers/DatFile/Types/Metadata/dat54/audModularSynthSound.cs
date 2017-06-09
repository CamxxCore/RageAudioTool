using System.IO;
using System.Linq;
using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audModularSynthSound : audSoundBase
    {      
        public audHashString OptAmpUnkHash { get; set; } //0x0-0x4

        public audHashString UnkHash1 { get; set; } //0x4-0x8

        public float UnkFloat { get; set; } //0x8-0xC

        public int UnkInt2 { get; set; } //0xC-0x10

        private int trackCount;

        [XmlArray]
        public audModularSynthSoundData[] UnkArrayData { get; set; } //0x28-..

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(OptAmpUnkHash.HashKey);

                    writer.Write(UnkHash1.HashKey);

                    writer.Write(UnkFloat);

                    writer.Write(UnkInt2);

                    writer.Write(trackCount);

                    for (int i = 0; i < AudioTracks.Count; i++)
                    {
                        writer.Write(AudioTracks[i]);
                    }

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
                OptAmpUnkHash = new audHashString(parent, reader.ReadUInt32()); //0x0-0x4

                UnkHash1 = new audHashString(parent, reader.ReadUInt32()); //0x4-0x8

                UnkFloat = reader.ReadSingle(); //0x8-0xC

                UnkInt2 = reader.ReadInt32(); //0xC-0x10

                trackCount = reader.ReadInt32(); //0x10-0x14

                for (int i = 0; i < 4; i++)
                {
                    AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                        bytesRead + ((int)reader.BaseStream.Position - 4));
                }

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

        public audModularSynthSoundData()
        { }

        public audModularSynthSoundData(audHashString unkHash, audHashString parameterHash, float value)
        {
            UnkHash = unkHash;
            ParameterHash = parameterHash;
            Value = value;
        }
    }
}
