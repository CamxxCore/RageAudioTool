using System;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audFloat : audFiletypeValue<float>
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
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

            Value = BitConverter.ToSingle(data, bytesRead);

            return data.Length;
        }

        public audFloat(RageDataFile parent) : base(parent)
        { }

        public audFloat(RageDataFile parent, float data) : base(parent, data)
        { }

        public audFloat(RageDataFile parent, string name) : base(parent, name)
        { }

        public audFloat(RageDataFile parent, string name, float data) : base(parent, name, data)
        { }

        public audFloat(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }
    }
}
