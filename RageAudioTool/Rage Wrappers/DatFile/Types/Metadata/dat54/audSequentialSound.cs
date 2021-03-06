﻿using System.IO;
using System.Text;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSequentialSound : audSoundBase
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write((byte)AudioTracks.Count);

                    for (int i = 0; i < AudioTracks.Count; i++)
                    {
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
                int numItems = reader.ReadByte();

                for (int i = 0; i < numItems; i++)
                {
                    AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                        bytesRead + ((int)reader.BaseStream.Position - 4));
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < AudioTracks.Count; i++)
            {
                builder.AppendLine("Hash " + (i + 1) + ": " + AudioTracks[i]);
            }

            return builder.ToString();
        }

        public audSequentialSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSequentialSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSequentialSound()
        { }
    }
}
