using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVoiceData : audFiletypeValue<byte[]>
    {
        [XmlElement(DataType = "hexBinary")]
        public override byte[] Value { get; set; }

        public override byte[] Serialize()
        {
            return Value;
        }

        public override int Deserialize(byte[] data)
        {
            Value = data;

            return data.Length;
        }

        public override string ToString()
        {
            return BitConverter.ToString(Value).Replace("-", "");
        }

        public audVoiceData(RageDataFile parent) : base(parent)
        { }

        public audVoiceData(RageDataFile parent, byte[] data) : base(parent, data)
        { }

        public audVoiceData(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }

        public audVoiceData(RageDataFile parent, string name) : base(parent, name)
        { }

        public audVoiceData(RageDataFile parent, string name, byte[] data) : base(parent, name, data)
        { }

        public audVoiceData() : base(null)
        { }
    }
}
