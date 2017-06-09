using System.IO;
using System.ComponentModel;
using RageAudioTool.IO;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audMathOperationSound : audSoundBase
    {
        public audMathOperationSoundData[] UnkData { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]);

                    writer.Write((byte) UnkData.Length);

                    for (int i = 0; i < UnkData.Length; i++)
                    {
                        writer.Write(UnkData[i].UnkByte);

                        writer.Write(UnkData[i].UnkInt);

                        writer.Write(UnkData[i].UnkInt1);

                        writer.Write(UnkData[i].UnkInt2);

                        writer.Write(UnkData[i].UnkInt3);

                        writer.Write(UnkData[i].UnkInt4);

                        writer.Write(UnkData[i].ParameterHash.HashKey);

                        writer.Write(UnkData[i].ParameterHash1.HashKey);
                    }
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                var itemsCount = reader.ReadByte();

                UnkData = new audMathOperationSoundData[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    UnkData[i] = new audMathOperationSoundData
                    {
                        UnkByte = reader.ReadByte(),
                        UnkInt = reader.ReadInt32(),
                        UnkInt1 = reader.ReadInt32(),
                        UnkInt2 = reader.ReadInt32(),
                        UnkInt3 = reader.ReadInt32(),
                        UnkInt4 = reader.ReadInt32(),
                        ParameterHash = new audHashString(parent, reader.ReadUInt32()),
                        ParameterHash1 = new audHashString(parent, reader.ReadUInt32())
                    };
                }

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audMathOperationSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audMathOperationSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audMathOperationSound()
        { }
    }

    [TypeConverter(typeof(NamedObjectConverter))]
    public class audMathOperationSoundData
    {
        public audMathOperationSoundData(byte unkByte, int unkInt, int unkInt1, int unkInt2, int unkInt3, int unkInt4, audHashString parameterHash, audHashString parameterHash1)
        {
            UnkByte = unkByte;
            UnkInt = unkInt;
            UnkInt1 = unkInt1;
            UnkInt2 = unkInt2;
            UnkInt3 = unkInt3;
            UnkInt4 = unkInt4;
            ParameterHash = parameterHash;
            ParameterHash1 = parameterHash1;
        }

        public audMathOperationSoundData()
        { }

        public byte UnkByte { get; set; } //0x0-0x1

        public int UnkInt { get; set; } //0x1-0x5

        public int UnkInt1 { get; set; } //0x5-0x9

        public int UnkInt2 { get; set; } //0x9-0xD

        public int UnkInt3 { get; set; } //0xD-0x11

        public int UnkInt4 { get; set; } //0x11-0x15

        public audHashString ParameterHash { get; set; } //0x15-0x19

        public audHashString ParameterHash1 { get; set; } //0x19-0x1D
    }

}
