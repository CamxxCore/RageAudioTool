using System;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audInteger : audFiletypeValue<int>
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

            Value = BitConverter.ToInt32(data, bytesRead);
            return data.Length;
        }

        public audInteger(RageDataFile parent) : base(parent)
        { }

        public audInteger(RageDataFile parent, int data) : base(parent, data)
        { }

        public audInteger(RageDataFile parent, string name) : base(parent, name)
        { }

        public audInteger(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }

        public audInteger(RageDataFile parent, string name, int data) : base(parent, name, data)
        { }
    }
}
