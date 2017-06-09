using System.IO;
using System.Text;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audWrapperSound : audSoundBase
    {
        //public audHashString SoundHash { get; set; } //0x0-0x4

        // maybe start delay?
        public int FrameStartTime { get; set; } //0x4-0x8

       // public audHashString SoundHash1 { get; set; } //0x8-0xC

        // My guess is that this is related to the time at which a child sound should start playin (or the length of the sound).
        public short FrameTimeInterval { get; set; } //0xC-0xE

        public audHashString[] Variables { get; set; } //0xF

        public  byte[] UnkByteData { get; set; } // ...

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]); //0x0-0x4

                    writer.Write(FrameStartTime); //0x4-0x8

                    writer.Write(AudioTracks[1]); //0x8-0xC

                    writer.Write(FrameTimeInterval); //0xC-0xE

                    writer.Write((byte)Variables.Length); //0xE-0xF

                    for (int i = 0; i < Variables.Length; i++)
                    {
                        writer.Write(Variables[i].HashKey);
                    }

                    for (int i = 0; i < UnkByteData.Length; i++)
                    {
                        writer.Write(UnkByteData[i]);
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
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), bytesRead + ((int)reader.BaseStream.Position - 4));

                FrameStartTime = reader.ReadInt32();

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), bytesRead + ((int)reader.BaseStream.Position - 4));

                FrameTimeInterval = reader.ReadInt16();

                int itemsCount = reader.ReadByte();

                Variables = new audHashString[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Variables[i] = new audHashString(parent, reader.ReadUInt32());
                }

                UnkByteData = new byte[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    UnkByteData[i] = reader.ReadByte();
                }

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Sound Hash: " + AudioTracks[0]);

            builder.AppendLine("Start Time: " + FrameStartTime);

            builder.AppendLine("Sound Hash 1: " + AudioTracks[1]);

            builder.AppendLine("Interval: " + FrameTimeInterval);

            for (int i = 0; i < Variables.Length; i++)
            {
                builder.AppendLine("Hash " + (i + 1) + ": " + Variables[i]);
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
