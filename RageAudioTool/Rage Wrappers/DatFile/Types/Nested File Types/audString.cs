using System;
using System.Text;
using System.Linq;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audString : audFiletypeBase<string>
    {
        public override int Deserialize(byte[] data)
        {
            Data = Encoding.ASCII.GetString(data.TakeWhile(b => !b.Equals(0)).ToArray());

            return data.Length;
        }

        public audString()
        { }

        public audString(string name) : base(name)
        { }

        public audString(string name, string data) : base(name, data)
        { }
    }
}
