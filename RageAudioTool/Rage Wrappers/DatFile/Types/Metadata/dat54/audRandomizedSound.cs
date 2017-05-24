using System;
using System.Xml.Serialization;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audRandomizedSound : audSoundBase
    {
        byte ItemCount { get; set; } //0x0-0x1

        byte DataStart{ get; set; } //0x1-0x2

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
            }

            return data.Length;
        }


        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audRandomizedSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audRandomizedSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audRandomizedSound()
        { }
    }
}
