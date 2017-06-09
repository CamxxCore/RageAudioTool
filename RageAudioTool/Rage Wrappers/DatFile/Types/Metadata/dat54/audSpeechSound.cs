using System;
using System.Xml.Serialization;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSpeechSound : audSoundBase
    {
        public int UnkInt { get; set; } //maybe file index?

        public int UnkInt1 { get; set; } //ox4-0x8

        public audHashString VoiceDataHash { get; set; } //0x8-0xC

        public string SpeechName { get; set; } //0xD-...

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(UnkInt);

                    writer.Write(UnkInt1);

                    writer.Write(VoiceDataHash.HashKey);

                    writer.Write(SpeechName);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                UnkInt = reader.ReadInt32();

                UnkInt1 = reader.ReadInt32();

                VoiceDataHash = new audHashString(parent, reader.ReadUInt32());

                SpeechName = reader.ReadString();

                return (int)reader.BaseStream.Position;
            }
        }

        public audSpeechSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audSpeechSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSpeechSound()
        { }
    }
}
