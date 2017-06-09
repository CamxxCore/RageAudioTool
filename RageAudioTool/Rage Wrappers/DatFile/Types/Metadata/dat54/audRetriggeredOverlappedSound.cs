using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audRetriggeredOverlappedSound : audSoundBase
    {
        public ushort UnkShort { get; set; } //0x0-0x2

        public ushort UnkShort1 { get; set; } //0x2-0x4

        public ushort UnkShort2 { get; set; } //0x4-0x6

        public ushort UnkShort3 { get; set; } // 0x6-0x8

        public audHashString ParameterHash { get; set; } //0x8-0xC

        public audHashString ParameterHash1 { get; set; } //0xC-0x10

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkShort);

                    writer.Write(UnkShort1);

                    writer.Write(UnkShort2);

                    writer.Write(UnkShort3);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(AudioTracks[0]);

                    writer.Write(AudioTracks[1]);

                    writer.Write(AudioTracks[2]);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                UnkShort = reader.ReadUInt16();

                UnkShort1 = reader.ReadUInt16();

                UnkShort2 = reader.ReadUInt16();

                UnkShort3 = reader.ReadUInt16();

                ParameterHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audRetriggeredOverlappedSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audRetriggeredOverlappedSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audRetriggeredOverlappedSound()
        { }
    }
}
