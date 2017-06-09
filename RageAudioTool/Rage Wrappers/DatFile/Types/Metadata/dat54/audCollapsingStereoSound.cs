using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audCollapsingStereoSound : audSoundBase
    {
        public float UnkFloat { get; set; }

        public float UnkFloat1 { get; set; }

        public audHashString ParameterHash { get; set; } //0x10-0x14

        public audHashString ParameterHash1 { get; set; } //0x14-0x18

        public audHashString ParameterHash2 { get; set; } //0x18-0x1C

        public audHashString ParameterHash3 { get; set; } //0x1C-0x20

        public audHashString ParameterHash4 { get; set; } //0x20-0x24

        public audHashString ParameterHash5 { get; set; } //0x28-0x2C

        public int UnkInt { get; set; } //0x24-0x28

        public byte UnkByte { get; set; } //0x2c-0x2D

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]); //0x0-0x4

                    writer.Write(AudioTracks[1]); //0x4-0x8

                    writer.Write(UnkFloat);

                    writer.Write(UnkFloat1);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(ParameterHash2.HashKey);

                    writer.Write(ParameterHash3.HashKey);

                    writer.Write(ParameterHash4.HashKey);

                    writer.Write(UnkInt);

                    writer.Write(ParameterHash5.HashKey);

                    writer.Write(UnkByte);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), bytesRead + ((int)reader.BaseStream.Position - 4));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), bytesRead + ((int)reader.BaseStream.Position - 4));

                UnkFloat = reader.ReadSingle(); //0x8

                UnkFloat1 = reader.ReadSingle(); //0xC

                ParameterHash = new audHashString(parent, reader.ReadUInt32()); //0x10

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32()); //0x14

                ParameterHash2 = new audHashString(parent, reader.ReadUInt32()); //0x18

                ParameterHash3 = new audHashString(parent, reader.ReadUInt32()); //0x1C

                ParameterHash4 = new audHashString(parent, reader.ReadUInt32()); //0x20

                UnkInt = reader.ReadInt32(); //0x24-0x28

                ParameterHash5 = new audHashString(parent, reader.ReadUInt32()); //0x28-0x2C

                UnkByte = reader.ReadByte(); //0x2C-0x2D

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audCollapsingStereoSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audCollapsingStereoSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audCollapsingStereoSound()
        { }
    }
}
