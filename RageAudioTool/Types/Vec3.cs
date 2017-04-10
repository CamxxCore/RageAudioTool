using System;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace RageAudioTool.Types
{
    [StructLayout(LayoutKind.Explicit, Size = 0x18)]
    [TypeConverter(typeof(ValueTypeConverter))]
    public struct Vec3
    {
        [FieldOffset(8)]
        [MarshalAs(UnmanagedType.R4)]
        public float X;
        [FieldOffset(12)]
        [MarshalAs(UnmanagedType.R4)]
        public float Y;
        [FieldOffset(16)]
        [MarshalAs(UnmanagedType.R4)]
        public float Z;

        public float this[int idx]
        {
            get
            {
                switch (idx)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        throw new ArgumentOutOfRangeException("idx");
                }
            }
        }

        public static implicit operator float[] (Vec3 v)
        {
            return new float[] { v.X, v.Y, v.Z };
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}]", X, Y, Z);
        }
    }
}
