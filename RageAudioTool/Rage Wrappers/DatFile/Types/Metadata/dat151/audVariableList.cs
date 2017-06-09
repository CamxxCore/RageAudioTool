using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVariableList : audFiletypeValue
    {
        public Pair<audHashString, byte[]>[] Items { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
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

                Items = new Pair<audHashString, byte[]>[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Items[i] = new Pair<audHashString, byte[]>
                    {
                        First = new audHashString(parent, reader.ReadUInt32()),
                        Second = reader.ReadBytes(4)
                    };
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
