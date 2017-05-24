using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audUnkSound : audSoundBase
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

        public audUnkSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audUnkSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audUnkSound()
        { }
    }
}
