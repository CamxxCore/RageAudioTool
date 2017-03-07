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

            builder.AppendLine("Unk Hash: 0x" + UnkHash.ToString("X"));

            builder.AppendLine("Wave Slot Num: " + WaveSlotIndex);

            return builder.ToString();
        }

        public audSimpleSound(string str) : base(str)
        { }

        public audSimpleSound(uint hashName) : base(hashName)
        { }

        public audSimpleSound()
        { }
    }
}
