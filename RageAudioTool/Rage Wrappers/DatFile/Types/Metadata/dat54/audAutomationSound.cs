using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audAutomationSound : audSoundBase
    {
        public float UnkFloat { get; set; } //0x4-0x8

        public float UnkFloat1 { get; set; } //0x8-0xC

        public audHashString ParameterHash { get; set; } //0xC-0x10

        public int WaveSlotId { get; set; } //0x14-0x18

        public audHashString UnkHash1 { get; set; } //0x18-0x1C

        // array data count 0x1C-0x20

        public Pair<int, audHashString>[] UnkArrayData { get; set; } //0x20-

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

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(AudioTracks[1]);

                    writer.Write(WaveSlotId);

                    writer.Write(UnkHash1.HashKey);

                    writer.Write(UnkArrayData.Length);

                    for (int i = 0; i < UnkArrayData.Length; i++)
                    {
                        writer.Write(UnkArrayData[i].First);

                        writer.Write(UnkArrayData[i].Second.HashKey);
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
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                UnkFloat = reader.ReadSingle();

                UnkFloat1 = reader.ReadSingle();

                ParameterHash = new audHashString(parent, reader.ReadUInt32());

                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()),
                    bytesRead + ((int)reader.BaseStream.Position - 4));

                WaveSlotId = reader.ReadInt32();

                UnkHash1 = new audHashString(parent, reader.ReadUInt32());

                var itemCount = reader.ReadInt32();

                UnkArrayData = new Pair<int, audHashString>[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
                    UnkArrayData[i] = new Pair<int, audHashString>(
                        reader.ReadInt32(),
                        new audHashString(parent, reader.ReadUInt32()));
                }
                
                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audAutomationSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audAutomationSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audAutomationSound()
        { }
    }
}
