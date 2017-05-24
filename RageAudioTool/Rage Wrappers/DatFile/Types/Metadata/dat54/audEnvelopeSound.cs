using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audEnvelopeSound : audSoundBase
    {
        public ushort UnkShortA { get; set; }

        public ushort UnkShortA1 { get; set; }

        public ushort UnkShortB { get; set; }

        public ushort UnkShortB1 { get; set; }

        public byte UnkByteA { get; set; }

        public byte UnkByteA1 { get; set; }

        public int UnkInt { get; set; }

        public ushort UnkShortC { get; set; }

        public int UnkIntA { get; set; }

        public int UnkIntA1 { get; set; }

        public audHashString CurvesUnkHash { get; set; }

        public audHashString CurvesUnkHash1 { get; set; }

        public audHashString CurvesUnkHash2 { get; set; }

        public audHashString ParameterHash { get; set; }

        public audHashString ParameterHash1 { get; set; }

        public audHashString ParameterHash2 { get; set; }

        public audHashString ParameterHash3 { get; set; }

        public audHashString ParameterHash4 { get; set; }

        public int UnkIntC { get; set; }

        public audHashString ParameterHash5 { get; set; }

        public float UnkFloat { get; set; }

        public int UnkIntE { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkShortA);

                    writer.Write(UnkShortA1);

                    writer.Write(UnkShortB);

                    writer.Write(UnkShortB1);

                    writer.Write(UnkByteA);

                    writer.Write(UnkByteA1);

                    writer.Write(UnkInt);

                    writer.Write(UnkShortC);

                    writer.Write(UnkIntA);

                    writer.Write(UnkIntA1);

                    writer.Write(CurvesUnkHash.HashKey);

                    writer.Write(CurvesUnkHash1.HashKey);

                    writer.Write(CurvesUnkHash2.HashKey);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(ParameterHash2.HashKey);

                    writer.Write(ParameterHash3.HashKey);

                    writer.Write(ParameterHash4.HashKey);

                    writer.Write(AudioTracks[0].HashKey);

                    writer.Write(UnkIntC);

                    writer.Write(ParameterHash5.HashKey);

                    writer.Write(UnkFloat);

                    writer.Write(UnkIntE);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                UnkShortA = reader.ReadUInt16(); //0x0-0x2

                UnkShortA1 = reader.ReadUInt16(); //0x2-0x4

                UnkShortB = reader.ReadUInt16(); //0x4-0x6

                UnkShortB1 = reader.ReadUInt16(); //0x6-0x8

                UnkByteA = reader.ReadByte(); //0x8-0x9

                UnkByteA1 = reader.ReadByte(); //0x9-0xA

                UnkInt = reader.ReadInt32(); //0xA-0xE

                UnkShortC = reader.ReadUInt16(); //0xE-0x10

                UnkIntA = reader.ReadInt32(); //0x10-0x14

                UnkIntA1 = reader.ReadInt32(); //0x14-0x18

                CurvesUnkHash = new audHashString(parent, reader.ReadUInt32()); //0x18-0x1C

                CurvesUnkHash1 = new audHashString(parent, reader.ReadUInt32()); //0x1C-0x20

                CurvesUnkHash2 = new audHashString(parent, reader.ReadUInt32()); //0x20-0x24

                ParameterHash = new audHashString(parent, reader.ReadUInt32()); //0x24-0x28

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32()); //0x28-0x2C

                ParameterHash2 = new audHashString(parent, reader.ReadUInt32()); //0x2C-0x30

                ParameterHash3 = new audHashString(parent, reader.ReadUInt32()); //0x30-0x34

                ParameterHash4 = new audHashString(parent, reader.ReadUInt32()); //0x34-0x38

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32())); //0x38-0x3C

                UnkIntC = reader.ReadInt32(); //0x3C-0x40

                ParameterHash5 = new audHashString(parent, reader.ReadUInt32()); //0x40-0x44

                UnkFloat = reader.ReadSingle(); //0x44-0x48

                UnkIntE = reader.ReadInt32(); //0x48-0x4C

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";// BitConverter.ToString(Data).Replace("-", "");
        }

        public audEnvelopeSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audEnvelopeSound()
        { }
    }
}
