using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundList : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            return Data;
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audSoundList(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSoundList(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSoundList()
        { }
    }
}
