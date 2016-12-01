using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace RageAudioTool
{
    public static class Utility
    {
        public static void DebugPrint(string msg)
        {
            Debug.Print(msg);
        }

        public static byte[] ToBytes(this string str)
        {
            return Encoding.ASCII.GetBytes(str);
        }
    }
}
