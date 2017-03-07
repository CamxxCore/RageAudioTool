using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class DatTypeFloatArr : DatTypeBase<float[]>
    {
        public override int ID
        {
            get
            {
                return 10;
            }
        }

        public DatTypeFloatArr(float[] data) : base(data)
        { }

        public DatTypeFloatArr(byte[] data) : base(data)
        { }

        public override string ToString()
        {
            var data = (float[]) Data;

            StringBuilder builder = new StringBuilder("{ ");

            for (int i = 0; i < data.Length; i++)
            {
                builder.AppendFormat("{0}, ", data[i]);
            }

            builder.Append("}");

            return builder.ToString();
        }
    }
}
