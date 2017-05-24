using System;
using System.IO;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audOnStopSound : audSoundBase
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0].HashKey);

                    writer.Write(AudioTracks[1].HashKey);

                    writer.Write(AudioTracks[2].HashKey);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()));

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audOnStopSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audOnStopSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audOnStopSound()
        { }
    }
}
