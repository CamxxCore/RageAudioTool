using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audLoopingSound : audSoundBase
    {
        public short UnkShort { get; set; } //0x0-0x2

        public short UnkShort1 { get; set; } //0x2-0x4

        public short UnkShort2 { get; set; } //0x4-0x6

        //audio track 0x6-0xA

        public audHashString ParameterHash { get; set; } //0xA-0xE

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

                    writer.Write(AudioTracks[0]);

                    writer.Write(ParameterHash.HashKey);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                UnkShort = reader.ReadInt16();

                UnkShort1 = reader.ReadInt16();

                UnkShort2 = reader.ReadInt16();

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                ParameterHash = new audHashString(parent, reader.ReadUInt32());
            }

            return data.Length;
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audLoopingSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audLoopingSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audLoopingSound()
        { }
    }
}
