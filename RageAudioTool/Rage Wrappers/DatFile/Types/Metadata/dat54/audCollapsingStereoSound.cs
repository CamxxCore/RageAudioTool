using System;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audCollapsingStereoSound : audSoundBase
    {
        public float UnkFloat { get; set; }

        public float UnkFloat1 { get; set; } //0x10-0x14

        public audHashString ParameterHash1 { get; set; } //0x14-0x18

        public audHashString ParameterHash2 { get; set; } //0x18-0x1C

        public audHashString ParameterHash3 { get; set; } //0x1C-0x20

        public audHashString ParameterHash4 { get; set; } //0x20-0x24

        public int UnkInt { get; set; } //0x24-0x28

        public float UnkFloat2 { get; set; } //0x28-0x2C

        public byte UnkByte { get; set; } //0x2c-0x2D

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(AudioTracks[0].HashKey); //0x0-0x4

                    writer.Write(AudioTracks[1].HashKey); //0x4-0x8

                    writer.Write(UnkFloat);

                    writer.Write(UnkFloat1);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(ParameterHash2.HashKey);

                    writer.Write(ParameterHash3.HashKey);

                    writer.Write(ParameterHash4.HashKey);

                    writer.Write(UnkInt);

                    writer.Write(UnkFloat2);

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
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                UnkFloat = reader.ReadSingle();

                UnkFloat1 = reader.ReadSingle();

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash2 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash3 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash4 = new audHashString(parent, reader.ReadUInt32());

                UnkInt = reader.ReadInt32();

                UnkFloat2 = reader.ReadSingle();

                UnkByte = reader.ReadByte();

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
