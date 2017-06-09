using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RageAudioTool.Rage_Wrappers.DatFile
{ 
    /// <summary>
    /// Metadata without strings e.g. speech resources, dat54 (usually)
    /// </summary>
    public class RageAudioMetadata5 : RageDataFile
    {
        public RageAudioMetadata5()
        {
            foreach (var file in Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + 
                "strings", 
                "*.txt", 
                SearchOption.AllDirectories))
            {
                using (var reader = File.OpenText(file))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            if (!Nametable.ContainsValue(line))
                            {
                                Nametable.Add(line.HashKey(), line);
                            }

                            else MessageBox.Show("Ignoring duplicate entry \"" + line + "\" in \"" + file);
                        }
                    }
                }
            }
        }

        public override audDataBase[] ReadDataItems(RageDataFileReadReference file, int itemCount)
        {
            var items = new audDataBase[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var hashKey = file.ReadUInt32();

                var offset = file.ReadInt32();

                var length = file.ReadInt32();

                byte[] data = new byte[length];

                Array.Copy(DataSection, offset, data, 0, length);

                items[i] = CreateDerivedDataType(data[0], hashKey);

                items[i].FileOffset = offset;

                items[i].Length = length;

                items[i].Deserialize(data);

            }
            return items;
        }

        protected override void WriteDataOffsets(RageDataFileWriteReference file)
        {
            for (int i = 0; i < DataItems.Length; i++)
            {
                file.Write(DataItems[i].Name.HashKey);

                file.Write(DataItems[i].FileOffset);

                file.Write(DataItems[i].Serialize().Length);
            }               
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private audDataBase CreateDerivedDataType(int dataType, uint hashKey)
        {
            if (Type == RageAudioMetadataFileType.Dat151Parameters)
            {
                switch ((Dat151AudMetadataTypes)dataType)
                {
                  //  case dat151_audMetadataTypes.ShoreLineLake:
                   //     return new audShorelineLake(this, hashKey);
                   // case dat151_audMetadataTypes.ShoreLinePool:
                     //   return new audShorelinePoolMetadata(this, hashKey);
                    case Dat151AudMetadataTypes.ShoreLineList:
                        return new audShorelineList(this, hashKey);
                    default:
                        return new audByteArray(this, hashKey);
                }
            }

            if (Type == RageAudioMetadataFileType.Dat54DataEntries)
            {
                switch ((Dat54AudMetadataTypes)dataType)
                {
                    case Dat54AudMetadataTypes.LoopingSound:
                        return new audLoopingSound(this, hashKey);
                    case Dat54AudMetadataTypes.MultitrackSound:
                        return new audMultitrackSound(this, hashKey);
                    case Dat54AudMetadataTypes.VariableBlockSound:
                        return new audVariableBlockSound(this, hashKey);
                    case Dat54AudMetadataTypes.SimpleSound:
                        return new audSimpleSound(this, hashKey);
                    case Dat54AudMetadataTypes.EnvelopeSound:
                        return new audEnvelopeSound(this, hashKey);
                    case Dat54AudMetadataTypes.TwinLoopSound:
                        return new audTwinLoopSound(this, hashKey);
                    case Dat54AudMetadataTypes.SpeechSound:
                        return new audSpeechSound(this, hashKey);
                    case Dat54AudMetadataTypes.OnStopSound:
                        return new audOnStopSound(this, hashKey);
                    case Dat54AudMetadataTypes.WrapperSound:
                        return new audWrapperSound(this, hashKey);
                    case Dat54AudMetadataTypes.SequentialSound:
                        return new audSequentialSound(this, hashKey);
                    case Dat54AudMetadataTypes.StreamingSound:
                        return new audStreamingSound(this, hashKey);
                    case Dat54AudMetadataTypes.RetriggeredOverlappedSound:
                        return new audRetriggeredOverlappedSound(this, hashKey);
                    case Dat54AudMetadataTypes.CrossfadeSound:
                        return new audCrossfadeSound(this, hashKey);
                    case Dat54AudMetadataTypes.CollapsingStereoSound:
                        return new audCollapsingStereoSound(this, hashKey);
                    case Dat54AudMetadataTypes.RandomizedSound:
                        return new audRandomizedSound(this, hashKey);
                    case Dat54AudMetadataTypes.EnvironmentSound:
                        return new audEnvironmentSound(this, hashKey);
                    case Dat54AudMetadataTypes.DynamicEntitySound:
                        return new audDynamicEntitySound(this, hashKey);
                    case Dat54AudMetadataTypes.SequentialOverlapSound:
                        return new audSequentialOverlapSound(this, hashKey);
                    case Dat54AudMetadataTypes.ModularSynthSound:
                        return new audModularSynthSound(this, hashKey);
                    case Dat54AudMetadataTypes.GranularSound:
                        return new audGranularSound(this, hashKey);
                    case Dat54AudMetadataTypes.DirectionalSound:
                        return new audDirectionalSound(this, hashKey);
                    case Dat54AudMetadataTypes.KineticSound:
                        return new audKineticSound(this, hashKey);
                    case Dat54AudMetadataTypes.SwitchSound:
                        return new audSwitchSound(this, hashKey);
                    case Dat54AudMetadataTypes.VariableCurveSound:
                        return new audVariableCurveSound(this, hashKey);
                    case Dat54AudMetadataTypes.VariablePrintValueSound:
                        return new audVariablePrintValueSound(this, hashKey);
                    case Dat54AudMetadataTypes.IfSound:
                        return new audIfSound(this, hashKey);
                    case Dat54AudMetadataTypes.MathOperationSound:
                        return new audMathOperationSound(this, hashKey);
                    case Dat54AudMetadataTypes.ParameterTransformSound:
                        return new audParameterTransformSound(this, hashKey);
                    case Dat54AudMetadataTypes.FluctuatorSound:
                        return new audFluctuatorSound(this, hashKey);
                    case Dat54AudMetadataTypes.AutomationSound:
                        return new audAutomationSound(this, hashKey);
                    case Dat54AudMetadataTypes.ExternalStreamSound:
                        return new audExternalStreamSound(this, hashKey);
                    case Dat54AudMetadataTypes.SoundSet:
                        return new audSoundSet(this, hashKey);
                    case Dat54AudMetadataTypes.SoundList:
                        return new audSoundList(this, hashKey);
                    case Dat54AudMetadataTypes.Unknown:
                        return new audUnknown(this, hashKey);
                    default:
                        return new audUnkSound(this, hashKey);
                }
            }

            if (Type == RageAudioMetadataFileType.Dat4)
            {
                return new audVoiceData(this, hashKey);
            }

            return null;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("File Type: " + Type);

            builder.AppendLine("String Section Length: " + StringTable.Length);

            if (StringTable.Length > 0)
            {
                builder.AppendLine();

                for (int i = 0; i < StringTable.Length; i++)
                {
                    builder.AppendFormat("{0} - {1}\n", i, StringTable[i]);
                }
            }

            builder.AppendLine("\n");

            builder.AppendLine("Data Entries Count: " + DataItems.Length);

            builder.AppendLine();

            foreach (audDataBase dataEntry in DataItems.OrderBy(x => x.FileOffset))
            {
                builder.AppendFormat("\n[{0}] {1} @ 0x{2:X}-0x{3:X}\n{4}\n", dataEntry.Name, dataEntry.GetType().Name, dataEntry.FileOffset, dataEntry.FileOffset + dataEntry.Serialize().Length, dataEntry);
            }

            builder.AppendLine();

            return builder.ToString();
        }
    }
}
