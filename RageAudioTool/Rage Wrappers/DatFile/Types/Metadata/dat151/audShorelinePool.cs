using System.IO;
using RageAudioTool.Rage_Wrappers.DatFile.Types.Metadata;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    struct FloatPair
    {
        public float A, B;
    }

    public class audShorelinePoolMetadata : audSoundBase
    {
        FloatPair[] _unkData;

        public audShorelinePoolMetadata(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audShorelinePoolMetadata()
        { }

        public override int Deserialize(byte[] data)
        {
            //var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                var count = reader.ReadInt16(); //0x40

                reader.ReadInt16();

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


                    //System.Windows.Forms.MessageBox.Show(unkData[i].A.ToString() + " " + unkData[i].B.ToString());

                }
            }

            return data.Length;
        }
    }
}
