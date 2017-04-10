using System;
using System.Linq;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public sealed class audFloatArray : audFiletypeValue<float[]>
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    var buffer = new byte[Value.Length * 4];

                    Buffer.BlockCopy(Value, 0, buffer, 0, buffer.Length);

                    writer.Write(buffer);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);
            Value = new float[data.Length / 4];
            Buffer.BlockCopy(data, bytesRead, Value, 0, data.Length - bytesRead);
            return data.Length;
        }

        public override string ToString()
        {
            return string.Join(" ", Value);
        }

        public audFloatArray(RageDataFile parent) : base(parent)
        { }

        public audFloatArray(RageDataFile parent, float[] data) : base(parent, data)
        { }

        public audFloatArray(RageDataFile parent, string name) : base(parent, name)
        { }

        public audFloatArray(RageDataFile parent, string name, float[] data) : base(parent, name, data)
        { }

        public audFloatArray(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }
    }
}
