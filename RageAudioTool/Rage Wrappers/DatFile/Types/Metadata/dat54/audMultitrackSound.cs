using System.Text;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audMultitrackSound : audSoundBase
    {
        public uint[] SoundNames { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write((byte)SoundNames.Length);

                    for (int i = 0; i < SoundNames.Length; i++)
                    {
                        writer.Write(SoundNames[i]);
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

                SoundNames = new uint[numItems];

                for (int i = 0; i < numItems; i++)
                {
                    SoundNames[i] = reader.ReadUInt32();
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < SoundNames.Length; i++)
            {
                builder.AppendLine("Hash " + (i + 1) + ": 0x" + SoundNames[i].ToString("X"));
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
