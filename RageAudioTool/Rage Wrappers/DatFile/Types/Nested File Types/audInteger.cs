using System;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audInteger : audFiletypeBase<int>
    {
        public override int Deserialize(byte[] data)
        {
            Data = BitConverter.ToInt32(data, 0);
            return data.Length;
        }

        public audInteger()
        { }

        public audInteger(string name) : base(name)
        { }

        public audInteger(int data) : base(data)
        { }

        public audInteger(string name, int data) : base(name, data)
        { }
    }
}
