using System;
using System.Text;
using System.IO;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundDef
    {
        public audHashString ScriptName { get; set; }

        public audHashString SoundName { get; set; }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Script Name: " + ScriptName);

            builder.AppendLine("Sound Name: " + SoundName);

            return builder.ToString();
        }
    }
}

