using System.IO;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audVariablePrintValueSound : audSoundBase
    {
        public audHashString ParameterHash { get; set; } //0x0-0x4

        public string VariableString { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);
                    writer.Write(ParameterHash.HashKey);
                    writer.Write(VariableString);
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                ParameterHash = new audHashString(parent, reader.ReadUInt32());
                VariableString = reader.ReadString();
                return (int)reader.BaseStream.Position;
            }
        }

        public override string ToString()
        {
            return "";//BitConverter.ToString(Data).Replace("-", "");
        }

        public audVariablePrintValueSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audVariablePrintValueSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audVariablePrintValueSound()
        { }
    }
}
