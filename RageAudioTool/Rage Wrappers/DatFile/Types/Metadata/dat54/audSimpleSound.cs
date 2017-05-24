using System.Text;
using System.IO;
using System.ComponentModel;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSimpleSound : audSoundBase
    {
        [Description("Name of the .wav file")]
        public audHashString FileName { get; set; }

        [Description("Relative path to parent wave container (i.e. STREAMED_VEHICLES_GRANULAR/dune_space)")]
        public audHashString ContainerName { get; set; }

        [Description("ID of wave (.awc) container")]
        public ushort WaveSlotId { get; set; }

        [Description("Internal index of wave (.awc) container")]
        public byte WaveSlotIndex { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {               
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(ContainerName.HashKey);

                    writer.Write(FileName.HashKey);

                    writer.Write(WaveSlotIndex);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                ContainerName = new audHashString(parent, reader.ReadUInt32());

                WaveSlotId = (ushort)(ContainerName.HashKey & 0xFFFF);

                FileName = new audHashString(parent, reader.ReadUInt32());

                WaveSlotIndex = reader.ReadByte();
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Wave Slot ID: " + WaveSlotId);

            builder.AppendLine("File Name: 0x" + FileName.ToString());

            builder.AppendLine("Wave Slot Num: " + WaveSlotIndex);

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
