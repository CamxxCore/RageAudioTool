using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVector : audFiletypeValue
    {
        public float X { get; set; }

        public float Y { get; set; }

        public float Z { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(new char[0x8]);
                    writer.Write(X);
                    writer.Write(Y);
                    writer.Write(Z);
                    writer.Write(new char[0x4]);
                }

                return stream.ToArray();
            }
        }

        public override unsafe int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                reader.ReadBytes(8);
                X = reader.ReadSingle();
                Y = reader.ReadSingle();
                Z = reader.ReadSingle();
                reader.ReadBytes(4);
            }

            return data.Length;
        }

        public audVector(RageDataFile parent, string name) : base(parent, name)
        { }

        public audVector(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }

        public audVector(RageDataFile parent) : this(parent, null)
        { }
    }
}
