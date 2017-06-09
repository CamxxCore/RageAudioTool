using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audUnknown : audSoundBase
    {
        public int UnkInt { get; set; }

        public audUnknownSoundData[] UnkData { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write((byte)UnkData.Length);

                    for (int i = 0; i < UnkData.Length; i++)
                    {
                        writer.Write(UnkData[i].UnkByte);

                        writer.Write(UnkData[i].UnkByte1);

                        writer.Write(UnkData[i].UnkByte2);

                        writer.Write(AudioTracks[i]);
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
                var itemsCount = reader.ReadByte();

                UnkData = new audUnknownSoundData[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    UnkData[i] = new audUnknownSoundData(reader.ReadByte(), reader.ReadByte(), reader.ReadByte());

                    AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()),
                        bytesRead + ((int)reader.BaseStream.Position - 4));
                }

                return (int) reader.BaseStream.Position;
            }
        }


        public override string ToString()
        {
            return ""; //BitConverter.ToString(Data).Replace("-", "");
        }

        public audUnknown(RageDataFile parent, string str) : base(parent, str)
        {
        }

        public audUnknown(RageDataFile parent, uint hashName) : base(parent, hashName)
        {
        }

        public audUnknown()
        {
        }
    }


    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class audUnknownSoundData
    {
        public byte UnkByte { get; set; }

        public byte UnkByte1 { get; set; }

        public byte UnkByte2 { get; set; }

        public audUnknownSoundData()
        {  }

        public audUnknownSoundData(byte unkByte, byte unkByte1, byte unkByte2)
        {
            UnkByte = unkByte;
            UnkByte1 = unkByte1;
            UnkByte2 = unkByte2;
        }
    }
}
