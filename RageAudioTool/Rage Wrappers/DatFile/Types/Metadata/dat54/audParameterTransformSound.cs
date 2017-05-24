using System;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

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
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0].HashKey);

                    writer.Write(UnkArrayData.Length);

                    for (int i = 0; i < UnkArrayData.Length; i++)
                    {
                        writer.Write(UnkArrayData[i].ParameterHash.HashKey);

                        writer.Write(UnkArrayData[i].UnkFloat);

                        writer.Write(UnkArrayData[i].UnkFloat1);

                        writer.Write(UnkArrayData[i].NestedData.Length);

                        for (int x = 0; x < UnkArrayData[i].NestedData.Length; i++)
                        {
                            writer.Write(UnkArrayData[i].NestedData[x].UnkFloat);

                            writer.Write(UnkArrayData[i].NestedData[x].UnkInt);

                            writer.Write(UnkArrayData[i].NestedData[x].ParameterHash.HashKey);

                            writer.Write(UnkArrayData[i].NestedData[x].UnkFloat1);

                            writer.Write(UnkArrayData[i].NestedData[x].UnkFloat2);

                            writer.Write(UnkArrayData[i].NestedData[x].NestedItems.Length);

                            for (int y = 0; y < UnkArrayData[i].NestedData[x].NestedItems.Length; y++)
                            {
                                writer.Write(UnkArrayData[i].NestedData[x].NestedItems[y].First.HashKey);

                                writer.Write(UnkArrayData[i].NestedData[x].NestedItems[y].Second);
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
                AudioTracks.Add(new audHashString(parent, reader.ReadUInt32())); //0x0-0x4

                var itemCount = reader.ReadInt32(); //0x4-0x8

                UnkArrayData = new audParameterTransformData[itemCount];

                for (int i = 0; i < itemCount; i++)
                {
                    UnkArrayData[i] = new audParameterTransformData();

                    UnkArrayData[i].ParameterHash = new audHashString(parent, reader.ReadUInt32()); //item+0x0

                    UnkArrayData[i].UnkFloat = reader.ReadSingle(); //item+0x4

                    UnkArrayData[i].UnkFloat1 = reader.ReadSingle(); //item+0x8-0xC

                    var nestedItemCount = reader.ReadInt32(); //item+0xC-0x10
                
                    UnkArrayData[i].NestedData = new audParameterTransformDataNested[nestedItemCount];

                    for (int x = 0; x < nestedItemCount; x++)
                    {
                        UnkArrayData[i].NestedData[x] = new audParameterTransformDataNested();

                        UnkArrayData[i].NestedData[x].UnkFloat = reader.ReadSingle();

                        UnkArrayData[i].NestedData[x].UnkInt = reader.ReadInt32();

                        UnkArrayData[i].NestedData[x].ParameterHash = new audHashString(parent, reader.ReadUInt32());

                        UnkArrayData[i].NestedData[x].UnkFloat1 = reader.ReadSingle();

                        UnkArrayData[i].NestedData[x].UnkFloat2 = reader.ReadSingle();

                        var pairItemCount = reader.ReadInt32();

                        UnkArrayData[i].NestedData[x].NestedItems = new Pair<audHashString, float>[pairItemCount];

                        for (int y = 0; y < pairItemCount; y++)
                        {
                            UnkArrayData[i].NestedData[x].NestedItems[y] = new Pair<audHashString, float>();

                            UnkArrayData[i].NestedData[x].NestedItems[y].First = new audHashString(parent, reader.ReadUInt32());

                            UnkArrayData[i].NestedData[x].NestedItems[y].Second = reader.ReadSingle();
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

        public Pair<audHashString, float>[] NestedItems { get; set; } //0x18-...
    }
}
