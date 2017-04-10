using System;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVariableList : audFiletypeValue
    {
        public Pair<HashString, byte[]>[] Items { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write((short)Items.Length);

                    for (int i = 0; i < Items.Length; i++)
                    {
                        writer.Write(Items[i].First.HashKey);

                        writer.Write(Items[i].Second);
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
                int itemsCount = reader.ReadInt32();

                Items = new Pair<HashString, byte[]>[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Items[i] = new Pair<HashString, byte[]>();

                    Items[i].First = reader.ReadUInt32();

                    Items[i].Second = reader.ReadBytes(4);
                }
            }

            return data.Length;
        }

        public audVariableList(RageDataFile parent, string name) : base(parent, name)
        { }

        public audVariableList(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }
    }
}
