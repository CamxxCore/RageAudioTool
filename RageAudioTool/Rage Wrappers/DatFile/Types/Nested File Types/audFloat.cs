using System;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audFloat : audFiletypeBase<float>
    {
        public override int Deserialize(byte[] data)
        {
            Data = BitConverter.ToSingle(data, 0);
            return data.Length;
        }

        public audFloat()
        { }

        public audFloat(string name) : base(name)
        { }

        public audFloat(float data) : base(data)
        { }

        public audFloat(string name, float data) : base(name, data)
        { }
    }
}
