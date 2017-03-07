using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audShorelineLakeMetadata : audSoundBase
    {
      //  int dataCount;

        floatPair[] unkData;

        public audShorelineLakeMetadata(string str) : base(str)
        { }

        public audShorelineLakeMetadata(uint hashName) : base(hashName)
        { }

        public audShorelineLakeMetadata()
        { }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            using (BinaryReader reader = new BinaryReader(new MemoryStream(data, bytesRead, data.Length - bytesRead)))
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


                    System.Windows.Forms.MessageBox.Show(unkData[i].A.ToString() + " " + unkData[i].B.ToString());
                }
            }

            return data.Length;
        }
    }
}
