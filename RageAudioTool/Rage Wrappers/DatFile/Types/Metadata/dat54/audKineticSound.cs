using System.IO;
using System.ComponentModel;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audKineticSound : audSoundBase
    {
        [Description("Maybe kinetic force vector?")]
        public float UnkFloat { get; set; }

        public float UnkFloat1 { get; set; }

        public float UnkFloat2 { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]); //0x0-0x4

                    writer.Write(UnkFloat);

                    writer.Write(UnkFloat1);

                    writer.Write(UnkFloat2);
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

                UnkFloat = reader.ReadSingle();

                UnkFloat1 = reader.ReadSingle();

                UnkFloat2 = reader.ReadSingle();
            }

            return data.Length;
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audKineticSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audKineticSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audKineticSound()
        { }
    }
}
