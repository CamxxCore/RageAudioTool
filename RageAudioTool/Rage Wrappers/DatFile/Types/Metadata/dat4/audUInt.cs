using System;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audUInt : audFiletypeValue<uint>
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(Value);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);
            Value = BitConverter.ToUInt32(data, bytesRead);
            return data.Length;
        }

        public audUInt(RageDataFile parent) : base(parent)
        { }

        public audUInt(RageDataFile parent, uint data) : base(parent, data)
        { }

        public audUInt(RageDataFile parent, string name) : base(parent, name)
        { }

        public audUInt(RageDataFile parent, uint hashKey, uint data) : base(parent, hashKey, data)
        { }

        public audUInt(RageDataFile parent, string name, uint data) : base(parent, name, data)
        { }
    }
}
