using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audWrapperSound : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public HashString SoundHash { get; set; } //0x0-0x4

        // maybe start delay?
        public int FrameStartTime { get; set; } //0x4-0x8

        public HashString SoundHash1 { get; set; } //0x8-0xC

        // My guess is that this is related to the time at which a child sound should start playin (or the length of the sound).
        public short FrameTimeInterval { get; set; } //0xC-0xE

        public HashString[] Variables { get; set; } //0xF

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(SoundHash.HashKey);

                    writer.Write(FrameStartTime);

                    writer.Write(SoundHash1.HashKey);

                    writer.Write(FrameTimeInterval);

                    writer.Write((byte)Variables.Length);

                    for (int i = 0; i < Variables.Length; i++)
                    {
                        writer.Write(Variables[i].HashKey);
                    }
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            Data = data;

            int bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                SoundHash = reader.ReadUInt32();

                FrameStartTime = reader.ReadInt32();

                SoundHash1 = reader.ReadUInt32();

                FrameTimeInterval = reader.ReadInt16();

                int itemsCount = reader.ReadByte();

                Variables = new HashString[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Variables[i] = reader.ReadUInt32();
                }

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Sound Hash: " + SoundHash.ToString());

            builder.AppendLine("Start Time: " + FrameStartTime.ToString());

            builder.AppendLine("Sound Hash 1: " + SoundHash1.ToString());

            builder.AppendLine("Interval: " + FrameTimeInterval.ToString());

            for (int i = 0; i < Variables.Length; i++)
            {
                builder.AppendLine("Hash " + (i + 1) + ": " + Variables[i].ToString());
            }

            return builder.ToString();
        }

        public audWrapperSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audWrapperSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audWrapperSound()
        { }
    }
}
