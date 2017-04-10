using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundSet : audDataBase
    {
        public audSoundDef[] Items { get; set; }

        private byte type;

        public override byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(type);

                    writer.Write(0xAAAAAAAA);

                    writer.Write(Items.Length);

                    for (int i = 0; i < Items.Length; i++)
                    {
                        writer.Write(Items[i].GameName.HashKey);

                        writer.Write(Items[i].SoundName.HashKey);
                    }
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                type = reader.ReadByte();

                reader.ReadInt32();

                var itemsCount = reader.ReadInt32();

                Items = new audSoundDef[itemsCount];

                string str;

                for (int i = 0; i < itemsCount; i++)
                {
                    Items[i] = new audSoundDef();

                    Items[i].GameName = reader.ReadUInt32();

                    if (parent.Nametable.TryGetValue(Items[i].GameName, out str))
                    {
                        Items[i].GameName = str;
                    }

                    Items[i].SoundName = reader.ReadUInt32();

                    if (parent.Nametable.TryGetValue(Items[i].SoundName, out str))
                    {
                        Items[i].SoundName = str;
                    }
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Items.Length; i++)
            {
                builder.AppendLine("\n[Item " + i + "]");
                builder.AppendLine("Child 1: 0x" + Items[i].GameName.ToString());
                builder.AppendLine("Child 2: 0x" + Items[i].SoundName.ToString());
            }

            return builder.ToString();
        }

        public audSoundSet(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSoundSet(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSoundSet()
        { }
    }
}
