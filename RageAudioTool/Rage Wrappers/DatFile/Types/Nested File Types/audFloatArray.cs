using System;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public sealed class audFloatArray : audFiletypeBase<float[]>
    {
        public override int Deserialize(byte[] data)
        {
            Data = new float[data.Length / 4];
            Buffer.BlockCopy(data, 0, (float[]) Data, 0, data.Length);
            return data.Length;
        }

        public override string ToString()
        {
            return string.Join(" ", (float[]) Data);
        }

        public audFloatArray()
        { }

        public audFloatArray(string name) : base(name)
        { }

        public audFloatArray(float[] data) : base(data)
        { }

        public audFloatArray(string name, float[] data) : base(name, data)
        { }
    }
}
