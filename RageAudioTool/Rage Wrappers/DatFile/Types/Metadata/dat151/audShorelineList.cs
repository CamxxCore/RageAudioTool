﻿using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audShorelineList : audDataBase
    {
        public uint[] Items { get; set; }

        public short Unk { get; set; }

        private int _type;

        public audShorelineList(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audShorelineList()
        { }

        public override byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(_type);

                    writer.Write((short)Items.Length);

                    writer.Write(Unk);

                    for (int i = 0; i < Items.Length; i++)
                    {
                        writer.Write(Items[i]);
                    }
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                _type = reader.ReadInt32();

                int itemsCount = reader.ReadInt16();

                Unk = reader.ReadInt16();

                Items = new uint[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Items[i] = reader.ReadUInt32();
                }
            }

            return data.Length;
        }
    }
}
