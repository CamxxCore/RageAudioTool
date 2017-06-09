using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audIfSound : audSoundBase
    {
        public audHashString ParameterHash1 { get; set; }

        public byte UnkByte { get; set; }

        public float UnkFloat { get; set; }

        public audHashString ParameterHash2 { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]); //0x0-0x4

                    writer.Write(AudioTracks[1]); //0x4-0x8

                    writer.Write(ParameterHash1.HashKey); //0x8-0xC

                    writer.Write(UnkByte); //0xC-0xD

                    writer.Write(UnkFloat); //0xD-0x11

                    writer.Write(ParameterHash2.HashKey); //0x11-0x15
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

                UnkByte = reader.ReadByte();

                UnkFloat = reader.ReadSingle();

                ParameterHash2 = new audHashString(parent, reader.ReadUInt32());

                return (int)reader.BaseStream.Position;
            }

        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audIfSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audIfSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audIfSound()
        { }
    }
}
