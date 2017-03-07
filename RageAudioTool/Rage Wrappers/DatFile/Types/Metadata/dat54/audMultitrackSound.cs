using System.Text;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audMultitrackSound : audSoundBase
    {
        public uint[] Offsets { get; set; }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                int numItems = reader.ReadByte();

                Offsets = new uint[numItems];

                for (int i = 0; i < numItems; i++)
                {
                    Offsets[i] = reader.ReadUInt32();
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Offsets.Length; i++)
            {
                builder.AppendLine("Hash " + (i + 1) + ": 0x" + Offsets[i].ToString("X"));
            }

            return builder.ToString();
        }

        public audMultitrackSound(string str) : base(str)
        { }

        public audMultitrackSound(uint hashName) : base(hashName)
        { }

        public audMultitrackSound()
        { }
    }
}
