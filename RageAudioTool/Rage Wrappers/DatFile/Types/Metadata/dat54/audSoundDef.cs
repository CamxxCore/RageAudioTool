using System.Text;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audSoundSetItem
    {
        public audHashString ScriptName { get; set; } = 
            new audHashString();

        public audHashString SoundName { get; set; } = 
            new audHashString();

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("Script Name: " + ScriptName);

            builder.AppendLine("Sound Name: " + SoundName);

            return builder.ToString();
        }
    }
}
