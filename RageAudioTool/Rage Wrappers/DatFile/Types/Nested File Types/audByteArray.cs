using System;
using System.Xml;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audByteArray : audFiletypeBase<byte[]>
    {
        [XmlElement(DataType = "hexBinary")]
        public override object Data { get; set; }

        public override int Deserialize(byte[] data)
        {
            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return BitConverter.ToString((byte[])Data).Replace("-", "");
        }

        public audByteArray()
        { }

        public audByteArray(string name) : base(name)
        { }

        public audByteArray(byte[] data) : base(data)
        { }

        public audByteArray(string name, byte[] data) : base(name, data)
        { }
    }
}
