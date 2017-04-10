using System;
using System.Text;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundDef
    {
        public HashString GameName { get; set; }

        public HashString SoundName { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Game Name: " + GameName);

            builder.AppendLine("Internal Name: " + SoundName);

            return builder.ToString();
        }
    }
}

