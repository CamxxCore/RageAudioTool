using System;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile.Types;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundSet : audSoundBase
    {
        public audSoundDef[] Items { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {             
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(Items.Length);

                    for (int i = 0; i < Items.Length; i++)
                    {
                        writer.Write(Items[i].ScriptName.HashKey);

                        writer.Write(Items[i].SoundName.HashKey);
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
                var itemsCount = reader.ReadInt32();

                Items = new audSoundDef[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Items[i] = new audSoundDef();

                    Items[i].ScriptName = new audHashString(parent, reader.ReadUInt32());

                    Items[i].SoundName = new audHashString(parent, reader.ReadUInt32());
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
                builder.AppendLine("Child 1: 0x" + Items[i].ScriptName.ToString());
                builder.AppendLine("Child 2: 0x" + Items[i].SoundName.ToString());
            }

            return builder.ToString();
        }

        public audSoundSet(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSoundSet()
        { }
    }
}
