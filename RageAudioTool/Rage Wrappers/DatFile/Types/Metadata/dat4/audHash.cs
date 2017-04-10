using System;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audHash : audFiletypeValue<uint>
    {
        public override int Deserialize(byte[] data)
        {
            Value = BitConverter.ToUInt32(data, 0);

            return data.Length;
        }

        public override byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {

                    writer.Write(Value);
                }

                return stream.ToArray();
            }
        }

        public override string ToString()
        {
            return string.Format("0x{0}", Value.ToString("X"));
        }

        public audHash(RageDataFile parent) : base(parent)
        { }

        public audHash(RageDataFile parent, uint data) : base(parent, data)
        { }

        public audHash(RageDataFile parent, string name) : base(parent, name)
        { }

        public audHash(RageDataFile parent, uint hashKey, uint data) : base(parent, hashKey, data)
        { }

        public audHash(RageDataFile parent, string name, uint data) : base(parent, name, data)
        { }
    }
}
