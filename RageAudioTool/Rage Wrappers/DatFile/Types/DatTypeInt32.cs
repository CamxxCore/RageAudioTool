using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class DatTypeInt32 : RageAudioDatItem<int>
    {
        public override int ID
        {
            get
            {
                return 1;
            }
        }

        public DatTypeInt32(int data) : base(data)
        { }

        public DatTypeInt32(byte[] data) : base(data)
        { }
    }
}
