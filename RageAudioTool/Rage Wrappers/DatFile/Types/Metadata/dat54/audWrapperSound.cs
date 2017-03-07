using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audWrapperSound : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            Data = data;

            return data.Length;
        }

        public override byte[] Serialize()
        {
            return Data;
        }

        public override string ToString()
        {
            return BitConverter.ToString(Data).Replace("-", "");
        }

        public audWrapperSound(string str) : base(str)
        { }

        public audWrapperSound(uint hashName) : base(hashName)
        { }

        public audWrapperSound()
        { }
    }
}
