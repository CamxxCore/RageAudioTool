using System;
using System.Text;
using System.Linq;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVector : audFiletypeBase<Vec3>
    {
        public override unsafe int Deserialize(byte[] data)
        {
            Data = new Vec3()
            {
                X = BitConverter.ToSingle(data, 8),
                Y = BitConverter.ToSingle(data, 12),
                Z = BitConverter.ToSingle(data, 16),
            };

            return data.Length;
        }

        public audVector()
        { }

        public audVector(string name) : base(name)
        { }

        public audVector(Vec3 data) : base(data)
        { }

        public audVector(string name, Vec3 data) : base(name, data)
        { }
    }
}
