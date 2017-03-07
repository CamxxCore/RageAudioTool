using System.Text;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audEnvelopeSound : audSoundBase
    {
        public ushort Unk { get; set; }

        public ushort UnkSub1 { get; set; }

        public uint UnkSubHash { get; set; }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            return builder.ToString();
        }

        public audEnvelopeSound(string str) : base(str)
        { }

        public audEnvelopeSound(uint hashName) : base(hashName)
        { }

        public audEnvelopeSound()
        { }
    }
}
