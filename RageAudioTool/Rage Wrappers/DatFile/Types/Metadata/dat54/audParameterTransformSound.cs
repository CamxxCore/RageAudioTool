using System.IO;
using System.ComponentModel;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audParameterTransformSound : audSoundBase
    {
        public audParameterTransformData[] UnkArrayData { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]);

                    writer.Write(UnkArrayData.Length);

                    for (int i = 0; i < UnkArrayData.Length; i++)
                    {
                        writer.Write(UnkArrayData[i].ParameterHash.HashKey);

                        writer.Write(UnkArrayData[i].UnkFloat);

                        writer.Write(UnkArrayData[i].UnkFloat1);
              
                        if (UnkArrayData[i].NestedData != null)
                        {
                            writer.Write(UnkArrayData[i].NestedData.Length);

                            for (int x = 0; x < UnkArrayData[i].NestedData.Length; x++)
                            {
                                writer.Write(UnkArrayData[i].NestedData[x].UnkFloat);

                                writer.Write(UnkArrayData[i].NestedData[x].UnkInt);

                                writer.Write(UnkArrayData[i].NestedData[x].ParameterHash.HashKey);

                                writer.Write(UnkArrayData[i].NestedData[x].UnkFloat1);

                                writer.Write(UnkArrayData[i].NestedData[x].UnkFloat2);

                                writer.Write(UnkArrayData[i].NestedData[x].NestedItems.Length);

                                for (int y = 0; y < UnkArrayData[i].NestedData[x].NestedItems.Length; y++)
                                {
                                    writer.Write(UnkArrayData[i].NestedData[x].NestedItems[y].First);

                                    writer.Write(UnkArrayData[i].NestedData[x].NestedItems[y].Second);
                                }
                            }
                        }
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
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32()), 
                    bytesRead + ((int)reader.BaseStream.Position - 4)); //0x0-0x4

                var itemCount = reader.ReadInt32(); //0x4-0x8

                UnkArrayData = new audParameterTransformData[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
                    UnkArrayData[i] = new audParameterTransformData
                        {
                            ParameterHash = new audHashString(parent, reader.ReadUInt32()),
                            UnkFloat = reader.ReadSingle(),
                            UnkFloat1 = reader.ReadSingle()
                        };

                    var nestedItemCount = reader.ReadInt32();
                
                    UnkArrayData[i].NestedData = new audParameterTransformDataNested[nestedItemCount];

                    for (int x = 0; x < nestedItemCount; x++)
                    {
                        UnkArrayData[i].NestedData[x] = new audParameterTransformDataNested
                            {
                                UnkFloat = reader.ReadSingle(),
                                UnkInt = reader.ReadInt32(),
                                ParameterHash = new audHashString(parent, reader.ReadUInt32()),
                                UnkFloat1 = reader.ReadSingle(),
                                UnkFloat2 = reader.ReadSingle()
                            };

                        var pairItemCount = reader.ReadInt32();

                        UnkArrayData[i].NestedData[x].NestedItems = new Pair<float, float>[pairItemCount];

                        for (int y = 0; y < pairItemCount; y++)
                        {
                            UnkArrayData[i].NestedData[x].NestedItems[y] =
                                new Pair<float, float>(reader.ReadSingle(), reader.ReadSingle());
                        }
                    }
                }

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audParameterTransformSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audParameterTransformSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audParameterTransformSound()
        { }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class audParameterTransformData
    {
        public audHashString ParameterHash { get; set; } //0x0-0x4

        public float UnkFloat { get; set; } //0x4-0x8

        public float UnkFloat1 { get; set; } //0x8-0xC

        public audParameterTransformDataNested[] NestedData { get; set; } //0x10..
    }

    public class audParameterTransformDataNested
    {
        public float UnkFloat { get; set; } //0x0-0x4

        public int UnkInt { get; set; } //0x4

        public audHashString ParameterHash { get; set; } //0x8-0xC

        public float UnkFloat1 { get; set; } //0xC

        public float UnkFloat2 { get; set; } //0x10-0x14

        public Pair<float, float>[] NestedItems { get; set; } //0x18-...
    }
}
