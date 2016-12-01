using System;
using System.Text;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class RageAudioDatItem<T> : IRageAudioDatBase
    {
        public virtual object Data { get; }

        public virtual int ID { get; }
     
        public RageAudioDatItem(T data)
        {
            Data = data;
        }

        public RageAudioDatItem(byte[] data)
        {
            Data = (T)FromBytes(data);
        }

        public static implicit operator T(RageAudioDatItem<T> obj)
        {
            return (T) obj.Data;
        }

        public static implicit operator RageAudioDatItem<T>(T obj)
        {
            return new RageAudioDatItem<T>(obj);
        }

        public static RageAudioDatItem<T> FromBytes(byte[] source)
        {
            Type type = typeof(T);

            if (type == typeof(bool))
                return (T)(object)BitConverter.ToBoolean(source, 0);
            if (type == typeof(char))
                return (T)(object)BitConverter.ToChar(source, 0);
            if (type == typeof(string))
                return (T)(object)Encoding.ASCII.GetString(source, 0, source.Length);
            if (type == typeof(sbyte))
                return (T)(object)(sbyte)BitConverter.ToChar(source, 0);
            if (type == typeof(short))
                return (T)(object)BitConverter.ToInt16(source, 0);
            if (type == typeof(int))
                return (T)(object)BitConverter.ToInt32(source, 0);
            if (type == typeof(long))
                return (T)(object)BitConverter.ToInt64(source, 0);
            if (type == typeof(byte))
                return (T)(object)(byte)BitConverter.ToChar(source, 0);
            if (type == typeof(ushort))
                return (T)(object)BitConverter.ToUInt16(source, 0);
            if (type == typeof(uint))
                return (T)(object)BitConverter.ToUInt32(source, 0);
            if (type == typeof(ulong))
                return (T)(object)BitConverter.ToUInt64(source, 0);
            if (type == typeof(float))
                return (T)(object)BitConverter.ToSingle(source, 0);
            if (type == typeof(double))
                return (T)(object)BitConverter.ToDouble(source, 0);
            if (type == typeof(decimal))
            {
                var i1 = BitConverter.ToInt32(source, 0);
                var i2 = BitConverter.ToInt32(source, 4);
                var i3 = BitConverter.ToInt32(source, 8);
                var i4 = BitConverter.ToInt32(source, 12);
                return (T)(object)new decimal(new int[] { i1, i2, i3, i4 });
            }

            if (type == typeof(byte[]))
                return (T)(object)source;

                throw new ArgumentException("Unrecognized type.");
        }

        public override string ToString()
        {
            return Data.ToString();
        }
    }
}
