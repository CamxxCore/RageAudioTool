using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSequentialOverlapSound : audSoundBase
    {
        public ushort UnkShort { get; set; }

        public audHashString ParameterHash { get; set; } //0x2-0x6

        public audHashString ParameterHash1 { get; set; } //0x6-0xA

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkShort);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

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

                ParameterHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

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

        public audSequentialOverlapSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSequentialOverlapSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSequentialOverlapSound()
        { }
    }
}
