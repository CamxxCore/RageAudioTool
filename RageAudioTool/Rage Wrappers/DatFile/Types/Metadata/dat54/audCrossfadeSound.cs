using System;
using System.Xml.Serialization;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audCrossfadeSound : audSoundBase
    {
        public byte UnkByte { get; set; } //0x8-0x9

        public float UnkFloat { get; set; } //0x9-0xD

        public float UnkFloat1 { get; set; } //0xD-0x11
            
        public int UnkInt2 { get; set; } //0xD-0x15

        public audHashString UnkCurvesHash { get; set; } //0x15-0x19

        public audHashString ParameterHash { get; set; } //0x19-0x1D

        public audHashString ParameterHash1 { get; set; } //0x1D-0x21

        public audHashString ParameterHash2 { get; set; } //0x21-0x25

        public audHashString ParameterHash3 { get; set; } //0x25-0x29

        public audHashString ParameterHash4 { get; set; } //0x29-0x2D

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

                    writer.Write(UnkByte);

                    writer.Write(UnkFloat);

                    writer.Write(UnkFloat1);

                    writer.Write(UnkInt2);

                    writer.Write(UnkCurvesHash.HashKey);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(ParameterHash2.HashKey);

                    writer.Write(ParameterHash3.HashKey);

                    writer.Write(ParameterHash4.HashKey);                   
                }

                return stream.ToArray();
            }
        }
        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), bytesRead + ((int)reader.BaseStream.Position - 4));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), bytesRead + ((int)reader.BaseStream.Position - 4));

                UnkByte = reader.ReadByte();

                UnkFloat = reader.ReadSingle();

                UnkFloat1 = reader.ReadSingle();

                UnkInt2 = reader.ReadInt32();

                UnkCurvesHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash2 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash3 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash4 = new audHashString(parent, reader.ReadUInt32());

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audCrossfadeSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audCrossfadeSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audCrossfadeSound()
        { }
    }
}
