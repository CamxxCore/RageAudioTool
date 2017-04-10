using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    struct floatPair
    {
        public float A, B;
    }

    public class audShorelinePoolMetadata : audSoundBase
    {
        floatPair[] unkData;

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

                unkData = new floatPair[count];

                for (int i = 0; i < count; i++)
                {
                    var floatA = reader.ReadSingle();

                    var floatB = reader.ReadSingle();

                    unkData[i] = new floatPair
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
