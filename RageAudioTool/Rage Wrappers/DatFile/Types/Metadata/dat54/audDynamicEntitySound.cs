using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audDynamicEntitySound : audSoundBase
    {
        public audHashString[] UnkArrayData { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write((byte)UnkArrayData.Length);

                    for (int i = 0; i < UnkArrayData.Length; i++)
                    {
                        writer.Write(UnkArrayData[i].HashKey);
                    }
                }

                return stream.ToArray();
            }
        }
        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                var itemsCount = reader.ReadByte();

                UnkArrayData = new audHashString[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    UnkArrayData[i] = new audHashString(parent, reader.ReadUInt32());
                }

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audDynamicEntitySound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audDynamicEntitySound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audDynamicEntitySound()
        { }
    }
}
