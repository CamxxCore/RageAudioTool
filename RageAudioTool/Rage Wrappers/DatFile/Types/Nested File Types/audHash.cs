using System;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audHash : audFiletypeBase<uint>
    {
        public override int Deserialize(byte[] data)
        {
            Data = BitConverter.ToUInt32(data, 0);
            return data.Length;
        }

        public audHash()
        { }

        public audHash(string name) : base(name)
        { }

        public audHash(uint data) : base(data)
        { }

        public audHash(string name, uint data) : base(name, data)
        { }
    }
}
