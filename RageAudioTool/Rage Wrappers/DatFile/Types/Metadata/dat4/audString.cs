using System.IO;
using System.Linq;
using System.Text;
using RageAudioTool.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audString : audFiletypeValue<string>
    {
        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            using (MemoryStream stream = new MemoryStream())
            {
                using (IOBinaryWriter writer = new IOBinaryWriter(stream))
                {
                    writer.Write(bytes);

                    writer.Write(Encoding.ASCII.GetBytes(Value));
                }

                return stream.ToArray();
            }
        }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            Value = Encoding.ASCII.GetString(data.Skip(bytesRead).TakeWhile(b => !b.Equals(0)).ToArray());

            return data.Length;
        }

        public audString(RageDataFile parent) : base(parent)
        { }

        public audString(RageDataFile parent, string name) : base(parent, name)
        { }

        public audString(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }

        public audString(RageDataFile parent, string name, string data) : base(parent, name, data)
        { }
    }
}
