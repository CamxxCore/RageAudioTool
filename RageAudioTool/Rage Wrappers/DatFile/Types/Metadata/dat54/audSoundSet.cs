using System.Text;
using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundSet : audSoundBase
    {
        public audSoundSetItem[] Items { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {             
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(Items.Length);

                    for (int i = 0; i < Items.Length; i++)
                    {
                        writer.Write(Items[i].ScriptName.HashKey);

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
                var itemsCount = reader.ReadInt32();

                Items = new audSoundSetItem[itemsCount];

                for (int i = 0; i < itemsCount; i++)
                {
                    Items[i] = new audSoundSetItem
                    {
                        ScriptName = new audHashString(parent, reader.ReadUInt32()),
                        SoundName = new audHashString(parent, reader.ReadUInt32())
                    };

                    AudioTracks.Add(new audHashDesc(Items[i].SoundName,
                        bytesRead + ((int)reader.BaseStream.Position - 4)));
                }
            }

            return data.Length;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < Items.Length; i++)
            {
                builder.AppendLine("\n[Item " + i + "]");
                builder.AppendLine("Child 1: 0x" + AudioTracks[i].TrackName);
                builder.AppendLine("Child 2: 0x" + Items[i]);
            }

            return builder.ToString();
        }

        public audSoundSet(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audSoundSet()
        { }
    }
}
