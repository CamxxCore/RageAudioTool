using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audOnStopSound : audSoundBase
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]);

                    writer.Write(AudioTracks[1]);

                    writer.Write(AudioTracks[2]);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
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

        public audOnStopSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audOnStopSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audOnStopSound()
        { }
    }
}
