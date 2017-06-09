using System;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audHash : audFiletypeValue<audHashString>
    {
        public override int Deserialize(byte[] data)
        {
            Value = new audHashString(parent, BitConverter.ToUInt32(data, 0));
            return data.Length;
        }

        public override byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(Value.HashKey);
                }

                return stream.ToArray();
            }
        }

        public audHash(RageDataFile parent) : base(parent)
        { }

        public audHash(RageDataFile parent, uint data) : base(parent, data)
        { }

        public audHash(RageDataFile parent, uint hashKey, audHashString data) : base(parent, hashKey, data)
        { }
    }
}
