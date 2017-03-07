using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace RageAudioTool
{
    public static class Utility
    {
        public static void DebugPrint(string msg)
        {
            Debug.Print(msg);
        }

        public static byte[] ToBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static uint HashKey(this string text)
        {
            uint hash = 0;

            char[] key = text.ToCharArray();

            for (int i = 0; i < key.Length; ++i)
            {
                hash += key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return (hash & 0xFFFFFFFF);
        }

        public static uint GenerateHash(string text)
        {
            uint hash = 0;

            char[] key = text.ToCharArray();

            for (int i = 0; i < key.Length; ++i)
            {
                hash += key[i];
                hash += (hash << 10);
                hash ^= (hash >> 6);
            }

            hash += (hash << 3);
            hash ^= (hash >> 11);
            hash += (hash << 15);

            return (hash & 0xFFFFFFFF);
        }

        public static int Size(this object type)
        {
            return Marshal.SizeOf(type);
        }
    }
}
