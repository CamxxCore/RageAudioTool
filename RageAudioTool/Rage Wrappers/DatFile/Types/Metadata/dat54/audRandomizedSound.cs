using System.ComponentModel;
using System.Linq;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audRandomizedSound : audSoundBase
    {
        public byte UnkByte { get; set; } //0x0-0x1 something count?

        [Browsable(false)]
        public byte DataStart { get; set; } //0x1-0x2

        public float[] AudioTrackUnkFloats { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkByte);

                    writer.Write(DataStart);

                    writer.Write(Enumerable.Repeat<byte>(0xFF, DataStart).ToArray());

                    writer.Write((byte)AudioTracks.Count);

                    for (int i = 0; i < AudioTracks.Count; i++)
                    {
                        writer.Write(AudioTracks[i]);

                        writer.Write(AudioTrackUnkFloats[i]);
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
                UnkByte = reader.ReadByte();

                DataStart = reader.ReadByte();

                reader.ReadBytes(DataStart);

                int numItems = reader.ReadByte();

                AudioTrackUnkFloats = new float[numItems];

                for (int i = 0; i < numItems; i++)
                {
                    AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                        bytesRead + ((int)reader.BaseStream.Position - 4));

                    AudioTrackUnkFloats[i] = reader.ReadSingle();
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audRandomizedSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audRandomizedSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audRandomizedSound()
        { }
    }
}
