using System.IO;
using System.ComponentModel;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audFluctuatorSound : audSoundBase
    {
        public audFluctuatorSoundData[] UnkArrayData { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]);

                    writer.Write(UnkArrayData.Length);

                    for (int i = 0; i < UnkArrayData.Length; i++)
                    {
                        writer.Write(UnkArrayData[i].UnkByte);

                        writer.Write(UnkArrayData[i].UnkByte1);

                        writer.Write(UnkArrayData[i].ParameterHash.HashKey);

                        writer.Write(UnkArrayData[i].UnkFloat);

                        writer.Write(UnkArrayData[i].UnkFloat1);

                        writer.Write(UnkArrayData[i].UnkFloat2);

                        writer.Write(UnkArrayData[i].UnkFloat3);

                        writer.Write(UnkArrayData[i].UnkFloat4);

                        writer.Write(UnkArrayData[i].UnkFloat5);

                        writer.Write(UnkArrayData[i].UnkFloat6);

                        writer.Write(UnkArrayData[i].UnkFloat7);

                        writer.Write(UnkArrayData[i].UnkFloat8);

                        writer.Write(UnkArrayData[i].UnkFloat9);

                        writer.Write(UnkArrayData[i].UnkFloat10);
                    }
                }

                return stream.ToArray();
            } 
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = 
                new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                var itemCount = reader.ReadInt32();

                UnkArrayData = new audFluctuatorSoundData[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
                    UnkArrayData[i] = new audFluctuatorSoundData
                    {
                        UnkByte = reader.ReadByte(),
                        UnkByte1 = reader.ReadByte(),
                        ParameterHash = new audHashString(parent, reader.ReadUInt32()),
                        UnkFloat = reader.ReadSingle(),
                        UnkFloat1 = reader.ReadSingle(),
                        UnkFloat2 = reader.ReadSingle(),
                        UnkFloat3 = reader.ReadSingle(),
                        UnkFloat4 = reader.ReadSingle(),
                        UnkFloat5 = reader.ReadSingle(),
                        UnkFloat6 = reader.ReadSingle(),
                        UnkFloat7 = reader.ReadSingle(),
                        UnkFloat8 = reader.ReadSingle(),
                        UnkFloat9 = reader.ReadSingle(),
                        UnkFloat10 = reader.ReadSingle()
                    };
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            return ""; // BitConverter.ToString(Data).Replace("-", "");
        }

        public audFluctuatorSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audFluctuatorSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audFluctuatorSound()
        { }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class audFluctuatorSoundData
    {
        public byte UnkByte { get; set; } //0x0-0x1

        public byte UnkByte1 { get; set; } //0x1-0x2

        public audHashString ParameterHash { get; set; } //0x2-0x6

        public float UnkFloat { get; set; } //0x6-0xA

        public float UnkFloat1 { get; set; } //0xA-0xE

        public float UnkFloat2 { get; set; } //0xE-0x12

        public float UnkFloat3 { get; set; } //0x12-0x16

        public float UnkFloat4 { get; set; } //0x16-0x1A

        public float UnkFloat5 { get; set; } //0x1A-0x1E

        public float UnkFloat6 { get; set; } //0x1E-0x22

        public float UnkFloat7 { get; set; } //0x22-0x26

        public float UnkFloat8 { get; set; } //0x26-0x2A

        public float UnkFloat9 { get; set; } //0x2A-0x2E

        public float UnkFloat10 { get; set; } //0x2E-0x32
    }
}
