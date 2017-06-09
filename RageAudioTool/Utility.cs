using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using RageAudioTool.IO;
using RageAudioTool.Rage_Wrappers.DatFile;

namespace RageAudioTool
{
    public static class Utility
    {
        public static void Write(this IOBinaryWriter writer, audHashString hash)
        {
            writer.Write(hash.HashKey);
        }

        public static void Write(this IOBinaryWriter writer, audHashDesc hashdesc)
        {
            hashdesc.Offset = (int)writer.BaseStream.Position;
            writer.Write(hashdesc.TrackName.HashKey);
        }

        public static void DebugPrint(string msg)
        {
            Debug.Print(msg);
        }

        public static byte[] ToBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }

        public static byte[] Join(IEnumerable<byte[]> arrays)
        {
            byte[] rv = new byte[arrays.Sum(a => a.Length)];
            int offset = 0;
            foreach (byte[] array in arrays)
            {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }

        public static uint HashKey(this string text)
        {
            uint hash = 0;

            char[] key = text.ToCharArray();

            for (int i = 0; i < key.Length; ++i)
            {
                hash += char.ToLower(key[i]);
                hash += hash << 10;
                hash ^= hash >> 6;
            }

            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return hash & 0xFFFFFFFF;
        }

        public static uint GenerateHash(string text)
        {
            uint hash = 0;

            char[] key = text.ToCharArray();

            for (int i = 0; i < key.Length; ++i)
            {
                hash += key[i];
                hash += hash << 10;
                hash ^= hash >> 6;
            }

            hash += hash << 3;
            hash ^= hash >> 11;
            hash += hash << 15;

            return hash & 0xFFFFFFFF;
        }

        public static int Size(this object type)
        {
            return Marshal.SizeOf(type);
        }
    }


    public class Pair<T1, T2>
    {
        public Pair()
        {}

        public Pair(T1 first, T2 second)
        {
            First = first;
            Second = second;
        }

        public T1 First { get; set; }
        public T2 Second { get; set; }
    }
}