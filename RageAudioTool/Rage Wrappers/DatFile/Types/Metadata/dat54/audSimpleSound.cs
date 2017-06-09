using System.Text;
using System.IO;
using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSimpleSound : audSoundBase
    {
        [Description("Name of the .wav file")]
        public audHashString FileName { get; set; }

        [XmlIgnore]
        [Description("Relative path to parent wave container (i.e. \"RESIDENT/animals\")")]
        public audHashString ContainerName { get; set; }

        [Description("Internal index of wave (.awc) container")]
        public byte WaveSlotNum { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioContainers[0]);
  
                    writer.Write(FileName.HashKey);

                    writer.Write(WaveSlotNum);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (var reader = new IOBinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                ContainerName = new audHashString(parent, reader.ReadUInt32());

                AudioContainers.Add(new audHashDesc(ContainerName, 
                    bytesRead + ((int)reader.BaseStream.Position - 4)));

                FileName = new audHashString(parent, reader.ReadUInt32());

                WaveSlotNum = reader.ReadByte();
            }
            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("File Name: " + FileName);
            builder.AppendLine("Container Name: " + ContainerName);
            builder.AppendLine("Wave Slot Num: " + WaveSlotNum);
            return builder.ToString();
        }

        public audSimpleSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSimpleSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSimpleSound()
        { }
    }
}
