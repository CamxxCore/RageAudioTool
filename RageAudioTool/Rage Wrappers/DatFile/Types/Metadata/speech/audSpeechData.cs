using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSpeechData : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override byte[] Serialize()
        {
            return Data;
        }

        public override int Deserialize(byte[] data)
        {
            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return BitConverter.ToString(Data).Replace("-", "");
        }

        public audSpeechData(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSpeechData(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSpeechData()
        { }
    }
}
