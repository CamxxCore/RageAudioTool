using System;
using System.IO;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public abstract class audShoreLine : audDataBase
    {
        public float OriginX { get; set; }

        public float OriginY { get; set; }

        public float MaxHorizontalExtent { get; set; }

        public float MaxVerticalExtent { get; set; }

        public int Unk { get; set; }

        public int Unk1 { get; set; }

        private int type;

        public audShoreLine(RageDataFile parent, string str) : base(parent, str)
        { }

        public audShoreLine(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audShoreLine()
        { }

        public override byte[] Serialize()
        {
            throw new NotImplementedException();
        }

        public override int Deserialize(byte[] data)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                type = reader.ReadInt32();

                reader.ReadInt32();

                OriginX = reader.ReadSingle();

                OriginY = reader.ReadSingle();

                MaxHorizontalExtent = reader.ReadSingle();

                MaxVerticalExtent = reader.ReadSingle();

                Unk = reader.ReadInt32();

                Unk1 = reader.ReadInt32();

                return (int)reader.BaseStream.Position;                
            }
        }
    }
}
