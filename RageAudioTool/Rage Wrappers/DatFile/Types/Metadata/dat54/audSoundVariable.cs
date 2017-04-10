using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundVariable : audDataBase
    {
        public float Value { get; set; }

        public float UnkFloat { get; set; }

        public byte Flags { get; set; }

        public override byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Name.HashKey);

                    writer.Write(Value);

                    writer.Write(UnkFloat);

                    writer.Write(Flags);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                uint hashKey = reader.ReadUInt32();

                string str;

                if (parent.Nametable.TryGetValue(hashKey, out str))
                {
                    Name = str;
                }

                else Name = hashKey;

                Value = reader.ReadSingle();

                UnkFloat = reader.ReadSingle();

                Flags = reader.ReadByte();

                return data.Length;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Name: " + Name);

            builder.AppendLine("Value: " + Value);

            builder.AppendLine("Unk Float: " + UnkFloat);

            builder.AppendLine("Flags: 0x" + Flags.ToString("X"));

            return builder.ToString();
        }

        public audSoundVariable(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSoundVariable(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSoundVariable()
        { }
    }
}
