using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class DatTypeFloat : RageAudioDatItem<float>
    {
        public override int ID
        {
            get
            {
                return 2;
            }
        }

        public DatTypeFloat(float data) : base(data)
        { }

        public DatTypeFloat(byte[] data) : base(data)
        { }
    }
}
