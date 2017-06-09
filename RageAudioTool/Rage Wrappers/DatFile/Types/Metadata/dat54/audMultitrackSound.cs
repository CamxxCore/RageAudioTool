using System.Text;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audMultitrackSound : audSoundBase
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            DataOffset = bytes.Length;

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

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
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < AudioTracks.Count; i++)
            {
                builder.AppendLine("Hash " + (i + 1) + ": " + AudioTracks[i]);
            }

            return builder.ToString();
        }

        public audMultitrackSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audMultitrackSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audMultitrackSound()
        { }
    }
}
