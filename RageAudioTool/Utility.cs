using System;
using System.Text;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using RageAudioTool.Types;

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

[TypeConverter(typeof(ValueTypeConverter))]
public struct HashString
{
    public uint HashKey { get; set; }

    public string HashName { get; set; }

    public HashString(uint hashKey)
    {
        HashKey = hashKey;
        HashName = string.Format("0x{0}", HashKey.ToString("X"));
    }

    public HashString(string hashName)
    {
        HashName = hashName;
        HashKey = RageAudioTool.Utility.HashKey(hashName);
    }

    public static implicit operator string(HashString hs)
    {
        return hs.HashName;
    }

    public static implicit operator uint(HashString hs)
    {
        return hs.HashKey;
    }

    public static implicit operator HashString(uint hashKey)
    {
        return new HashString(hashKey);
    }

    public static implicit operator HashString(string hashName)
    {
        return new HashString(hashName);
    }

    public override string ToString()
    {
        return HashName;
    }
}

public class Pair<T1, T2>
{
    public T1 First { get; set; }
    public T2 Second { get; set; }
}