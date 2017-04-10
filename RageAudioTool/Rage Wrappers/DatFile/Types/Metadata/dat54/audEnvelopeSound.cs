using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audEnvelopeSound : audSoundBase
    {
        //      public ushort Unk { get; set; }

        //    public ushort UnkSub1 { get; set; }

        //     public uint UnkSubHash { get; set; }

        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            Buffer.BlockCopy(bytes, 0, Data, 0, bytes.Length);

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
            return BitConverter.ToString(Data).Replace("-", "");
        }

        public audEnvelopeSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audEnvelopeSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audEnvelopeSound()
        { }
    }
}
