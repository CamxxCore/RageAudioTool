using System.IO;
using System.Text;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVariableBlockSound : audSoundBase
    {
        public int Unk;

        public audVariable[] Variables { get; set; }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                Unk = reader.ReadInt32();

                int numItems = reader.ReadByte();

                Variables = new audVariable[numItems];

                for (int i = 0; i < numItems; i++)
                {
                    Variables[i] = new audVariable();

                    Variables[i].HashName = reader.ReadUInt32();

                    Variables[i].DefaultValue = reader.ReadSingle();

                    Variables[i].Unk = reader.ReadSingle();

                    Variables[i].Flags = reader.ReadByte();          
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(string.Format("\nUnk Hash: 0x{0:X}", Unk));

            for (int i = 0; i < Variables.Length; i++)
            {
                builder.AppendLine("\n[Variable " + i + "]");
                builder.AppendLine("Hash: 0x" + Variables[i].HashName.ToString("X"));
                builder.AppendLine("Default Value: " + Variables[i].DefaultValue);
                builder.AppendLine("Secondary Value: " + Variables[i].Unk);       
            }

            return builder.ToString();
        }

        public audVariableBlockSound(string str) : base(str)
        { }

        public audVariableBlockSound(uint hashName) : base(hashName)
        { }

        public audVariableBlockSound()
        { }

        public struct audVariable
        {
            public uint HashName { get; set; }

            public float DefaultValue { get; set; }

            public float Unk { get; set; }

            public byte Flags { get; set; }
        }
    }
}
