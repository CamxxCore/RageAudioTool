using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audUnkMetadata : audSoundBase
    {
        floatPair[] unkData;

        public audUnkMetadata(string str) : base(str)
        { }

        public audUnkMetadata(uint hashName) : base(hashName)
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


        //            System.Windows.Forms.MessageBox.Show(unkData[i].A.ToString() + " " + unkData[i].B.ToString());
                }
            }

            return data.Length;
        }
    }
}
