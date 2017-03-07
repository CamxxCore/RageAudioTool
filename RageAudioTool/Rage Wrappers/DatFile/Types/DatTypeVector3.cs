using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class DatTypeVector3 : DatTypeBase<RageVector3>
    {
        public override int ID
        {
            get
            {
                return 5;
            }
        }

        public DatTypeVector3(RageVector3 data) : base(data)
        { }

        public DatTypeVector3(byte[] data) : base(data)
        { }
    }
}
