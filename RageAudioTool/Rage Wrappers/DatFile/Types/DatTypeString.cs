using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class DatTypeString : DatTypeBase<string>
    {
        public override int ID
        {
            get
            {
                return 3;
            }
        }

        public DatTypeString(string data) : base(data)
        { }

        public DatTypeString(byte[] data) : base(data)
        { }
    }
}
