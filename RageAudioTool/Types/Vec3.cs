using System.Runtime.InteropServices;

namespace RageAudioTool.Types
{
    [StructLayout(LayoutKind.Explicit, Size = 0x18)]
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

        public override string ToString()
        {
            return string.Format("[{0}, {1}, {2}]", X, Y, Z);
        }
    }
}
