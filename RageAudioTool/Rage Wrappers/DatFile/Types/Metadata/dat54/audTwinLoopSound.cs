using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audTwinLoopSound : audSoundBase
    {
        public ushort UnkShort { get; set; } //0x0-0x2

        public ushort UnkShort1 { get; set; } //0x2-0x4

        public ushort UnkShort2 { get; set; } //0x4-0x6

        public ushort UnkShort3 { get; set; } //0x6-0x8

        public audHashString UnkHash { get; set; } //0x8-0xC

        public audHashString ParameterHash { get; set; } //0xC-0x10

        public audHashString ParameterHash1 { get; set; } //0x10-0x14

        public audHashString ParameterHash2 { get; set; } //0x14-0x18

        public audHashString ParameterHash3 { get; set; } //0x18-0x1C

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkShort);

                    writer.Write(UnkShort1);

                    writer.Write(UnkShort2);

                    writer.Write(UnkShort3);

                    writer.Write(UnkHash.HashKey);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(ParameterHash2.HashKey);

                    writer.Write(ParameterHash3.HashKey);

                    writer.Write((byte)AudioTracks.Count);

                    for (int i = 0; i < AudioTracks.Count; i++)
                    {
                        writer.Write(AudioTracks[i]);
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
                UnkShort = reader.ReadUInt16();

                UnkShort1 = reader.ReadUInt16();

                UnkShort2 = reader.ReadUInt16();

                UnkShort3 = reader.ReadUInt16();

                UnkHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash2 = new audHashString(parent, reader.ReadUInt32());

                ParameterHash3 = new audHashString(parent, reader.ReadUInt32());

                int numItems = reader.ReadByte();

                for (int i = 0; i < numItems; i++)
                {
                    AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                        bytesRead + ((int)reader.BaseStream.Position - 4));
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            return "";// BitConverter.ToString(Data).Replace("-", "");
        }

        public audTwinLoopSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audTwinLoopSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audTwinLoopSound()
        { }
    }
}
