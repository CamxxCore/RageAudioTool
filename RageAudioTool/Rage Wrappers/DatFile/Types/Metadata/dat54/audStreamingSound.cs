using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audStreamingSound : audSoundBase
    {
        int UnkInt { get; set; } //0x0-0x4

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkInt);

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
                UnkInt = reader.ReadInt32();

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
            return ""; // BitConverter.ToString(Data).Replace("-", "");
        }

        public audStreamingSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audStreamingSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audStreamingSound()
        { }
    }
}
