using System.IO;
using RageAudioTool.Rage_Wrappers.DatFile.Types.Metadata;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audUnkMetadata : audSoundBase
    {
        FloatPair[] _unkData;

        public audUnkMetadata(RageDataFile parent, string str) : base(parent, str)
        { }

        public audUnkMetadata(RageDataFile parent, uint hashName) : base(parent,  hashName)
        { }

        public audUnkMetadata()
        { }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
            {
                reader.ReadInt32();

                var count = reader.ReadChar();

                _unkData = new FloatPair[count];

                for (int i = 0; i < count; i++)
                {
                    var floatA = reader.ReadSingle();

                    var floatB = reader.ReadSingle();

                    _unkData[i] = new FloatPair
                    {
                        A = floatA,
                        B = floatB
                    };


        //            System.Windows.Forms.MessageBox.Show(unkData[i].A.ToString() + " " + unkData[i].B.ToString());
                }
            }

            return data.Length;
        }
    }
}
