using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audShorelineLake : audShoreLine
    {
        public Pair<float, float>[] Children { get; set; }

        public audShorelineLake(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audShorelineLake()
        { }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                int itemsCount = reader.ReadInt32() / 2;

                Children = new Pair<float, float>[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Children[i] = new Pair<float, float>();

                    Children[i].First = reader.ReadSingle();

                    Children[i].Second = reader.ReadSingle();
                }
            }

            return data.Length;
        }
    }
}
