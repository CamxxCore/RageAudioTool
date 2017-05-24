using System.IO;
using System.Text;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVariableBlockSound : audSoundBase
    {
        public AudVariable[] Variables { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0].HashKey);

                    writer.Write((byte)Variables.Length);

                    for (int i = 0; i < Variables.Length; i++)
                    {
                        writer.Write(Variables[i].Name.HashKey);

                        writer.Write(Variables[i].Value);

                        writer.Write(Variables[i].UnkFloat);

                        writer.Write(Variables[i].Flags);
                    }
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                int numItems = reader.ReadByte();

                Variables = new AudVariable[numItems];

                for (int i = 0; i < numItems; i++)
                {
                    Variables[i] = new AudVariable();

                    Variables[i].Name = new audHashString(parent, reader.ReadUInt32());

                    Variables[i].Value = reader.ReadSingle();

                    Variables[i].UnkFloat = reader.ReadSingle();

                    Variables[i].Flags = reader.ReadByte();
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(string.Format("\nTrack Hash: 0x{0:X}", AudioTracks[0]));

            for (int i = 0; i < Variables.Length; i++)
            {
                builder.AppendLine("\n[Variable " + i + "]");
                builder.AppendLine("Name: " + Variables[i].Name.ToString());
                builder.AppendLine("Value: " + Variables[i].Value);
                builder.AppendLine("Unk Float: " + Variables[i].UnkFloat);
                builder.AppendLine("Flags: " + Variables[i].Flags);
            }

            return builder.ToString();
        }

        public audVariableBlockSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audVariableBlockSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audVariableBlockSound()
        { }
    }

    public class AudVariable
    {
        public audHashString Name { get; set; } = new audHashString();

        public float Value { get; set; }

        public float UnkFloat { get; set; }

        public byte Flags { get; set; }
    }
}