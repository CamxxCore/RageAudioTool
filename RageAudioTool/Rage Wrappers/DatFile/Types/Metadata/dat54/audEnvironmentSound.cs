using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audEnvironmentSound : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override byte[] Serialize()
        {
            //   var bytes = base.Serialize();

            /* using (MemoryStream stream = new MemoryStream())
             {
                 using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                 {

                 }

             }*/
            return Data;
        }

        public override int Deserialize(byte[] data)
        {
            //       int bytesRead = base.Deserialize(data);

            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return ""; //BitConverter.ToString(Data).Replace("-", "");
        }

        public audEnvironmentSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audEnvironmentSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audEnvironmentSound()
        { }
    }
}
