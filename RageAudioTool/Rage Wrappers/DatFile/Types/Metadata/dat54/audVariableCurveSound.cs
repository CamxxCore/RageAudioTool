using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVariableCurveSound : audSoundBase
    {
        public audHashString ParameterHash { get; set; } //0x4-0x8

        public audHashString ParameterHash1 { get; set; } //0x8-0xC

        public audHashString UnkCurvesHash { get; set; } //0xC-0x10

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(AudioTracks[0]);

                    writer.Write(ParameterHash.HashKey);

                    writer.Write(ParameterHash1.HashKey);

                    writer.Write(UnkCurvesHash.HashKey);
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

                ParameterHash = new audHashString(parent, reader.ReadUInt32());

                ParameterHash1 = new audHashString(parent, reader.ReadUInt32());

                UnkCurvesHash = new audHashString(parent, reader.ReadUInt32());

                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audVariableCurveSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audVariableCurveSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audVariableCurveSound()
        { }
    }
}
