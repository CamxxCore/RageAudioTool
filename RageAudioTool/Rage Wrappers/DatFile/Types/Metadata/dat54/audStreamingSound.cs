using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audStreamingSound : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return BitConverter.ToString(Data).Replace("-", "");
        }

        public audStreamingSound(string str) : base(str)
        { }

        public audStreamingSound(uint hashName) : base(hashName)
        { }

        public audStreamingSound()
        { }
    }
}
