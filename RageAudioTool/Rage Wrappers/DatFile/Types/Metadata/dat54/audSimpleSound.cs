using System.Text;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSimpleSound : audSoundBase
    {
        public uint UnkSubHash { get; set; }

        public ushort WaveSlotID { get; set; }

        public ushort UnkID { get; set; }

        public byte WaveSlotIndex { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {               
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(WaveSlotID);

                    writer.Write(UnkID);

                    writer.Write(UnkSubHash);

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
                WaveSlotID = reader.ReadUInt16();

                UnkID = reader.ReadUInt16();

                UnkSubHash = reader.ReadUInt32();

                WaveSlotIndex = reader.ReadByte();
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Wave Slot ID: " + WaveSlotID);

            builder.AppendLine("Unk ID: " + UnkID);

            builder.AppendLine("Unk Hash: 0x" + UnkSubHash.ToString("X"));

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
