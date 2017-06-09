using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audDirectionalSound : audSoundBase
    {
        public float UnkFloat { get; set; } //0x4-0x8

        public float UnkFloat1 { get; set; } //0x8-0xC

        public float UnkFloat2 { get; set; } //0xC-0x10

        public float UnkFloat3 { get; set; } //0x10-0x14

        public float UnkFloat4 { get; set; } //0x14-0x18

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]);

                    writer.Write(UnkFloat);

                    writer.Write(UnkFloat1);

                    writer.Write(UnkFloat2);

                    writer.Write(UnkFloat3);

                    writer.Write(UnkFloat4);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = 
                new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                UnkFloat = reader.ReadSingle();

                UnkFloat1 = reader.ReadSingle();

                UnkFloat2 = reader.ReadSingle();

                UnkFloat3 = reader.ReadSingle();

                UnkFloat4 = reader.ReadSingle();

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";// BitConverter.ToString(Data).Replace("-", "");
        }

        public audDirectionalSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audDirectionalSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audDirectionalSound()
        { }
    }
}
