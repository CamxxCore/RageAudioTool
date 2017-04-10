using System;
using System.IO;
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
        public override void Read(RageDataFileReadReference file)
        {
            string filePath = "categories.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader reader = File.OpenText(filePath))
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

                            else MessageBox.Show("Ignoring duplicate entry \"" + line + "\" in \"" + filePath);
                        }
                    }
                }
            }

            filePath = "variables.txt";

            if (File.Exists(filePath))
            {
                using (StreamReader reader = File.OpenText(filePath))
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

                            else MessageBox.Show("Ignoring duplicate entry \"" + line + "\" in \"" + filePath);
                        }
                    }
                }
            }

            base.Read(file);
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

                items[i].Deserialize(data);

                items[i].FileOffset = offset;
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
            if (Type == RageAudioMetadataFileType.Dat151_Parameters)
            {
                switch ((dat151_audMetadataTypes)dataType)
                {
                  //  case dat151_audMetadataTypes.ShoreLineLake:
                   //     return new audShorelineLake(this, hashKey);
                   // case dat151_audMetadataTypes.ShoreLinePool:
                     //   return new audShorelinePoolMetadata(this, hashKey);
                    case dat151_audMetadataTypes.ShoreLineList:
                        return new audShorelineList(this, hashKey);
                    default:
                        return new audByteArray(this, hashKey);
                }
            }

            else if (Type == RageAudioMetadataFileType.Dat54_DataEntries)
            {
                switch ((dat54_audMetadataTypes)dataType)
                {
                    case dat54_audMetadataTypes.LoopingSound:
                        return new audLoopingSound(this, hashKey);
                    case dat54_audMetadataTypes.MultitrackSound:
                        return new audMultitrackSound(this, hashKey);
                    case dat54_audMetadataTypes.VariableBlockSound:
                        return new audVariableBlockSound(this, hashKey);
                    case dat54_audMetadataTypes.SimpleSound:
                        return new audSimpleSound(this, hashKey);
                    case dat54_audMetadataTypes.EnvelopeSound:
                        return new audEnvelopeSound(this, hashKey);
                    case dat54_audMetadataTypes.TwinLoopSound:
                        return new audTwinLoopSound(this, hashKey);
                    case dat54_audMetadataTypes.SpeechSound:
                        return new audSpeechSound(this, hashKey);
                    case dat54_audMetadataTypes.OnStopSound:
                        return new audOnStopSound(this, hashKey);
                    case dat54_audMetadataTypes.WrapperSound:
                        return new audWrapperSound(this, hashKey);
                    case dat54_audMetadataTypes.SequentialSound:
                        return new audSequentialSound(this, hashKey);
                    case dat54_audMetadataTypes.StreamingSound:
                        return new audStreamingSound(this, hashKey);
                    case dat54_audMetadataTypes.RetriggeredOverlappedSound:
                        return new audRetriggeredOverlappedSound(this, hashKey);
                    case dat54_audMetadataTypes.CrossfadeSound:
                        return new audCrossfadeSound(this, hashKey);
                    case dat54_audMetadataTypes.CollapsingStereoSound:
                        return new audCollapsingStereoSound(this, hashKey);
                    case dat54_audMetadataTypes.RandomizedSound:
                        return new audRandomizedSound(this, hashKey);
                    case dat54_audMetadataTypes.EnvironmentSound:
                        return new audEnvironmentSound(this, hashKey);
                    case dat54_audMetadataTypes.DynamicEntitySound:
                        return new audDynamicEntitySound(this, hashKey);
                    case dat54_audMetadataTypes.SequentialOverlapSound:
                        return new audSequentialOverlapSound(this, hashKey);
                    case dat54_audMetadataTypes.ModularSynthSound:
                        return new audModularSynthSound(this, hashKey);
                    case dat54_audMetadataTypes.GranularSound:
                        return new audGranularSound(this, hashKey);
                    case dat54_audMetadataTypes.DirectionalSound:
                        return new audDirectionalSound(this, hashKey);
                    case dat54_audMetadataTypes.KineticSound:
                        return new audKineticSound(this, hashKey);
                    case dat54_audMetadataTypes.SwitchSound:
                        return new audSwitchSound(this, hashKey);
                    case dat54_audMetadataTypes.VariableCurveSound:
                        return new audVariableCurveSound(this, hashKey);
                    case dat54_audMetadataTypes.VariablePrintValueSound:
                        return new audVariablePrintValueSound(this, hashKey);
                    case dat54_audMetadataTypes.IfSound:
                        return new audIfSound(this, hashKey);
                    case dat54_audMetadataTypes.MathOperationSound:
                        return new audMathOperationSound(this, hashKey);
                    case dat54_audMetadataTypes.ParameterTransformSound:
                        return new audParameterTransformSound(this, hashKey);
                    case dat54_audMetadataTypes.FluctuatorSound:
                        return new audFluctuatorSound(this, hashKey);
                    case dat54_audMetadataTypes.AutomationSound:
                        return new audAutomationSound(this, hashKey);
                    case dat54_audMetadataTypes.ExternalStreamSound:
                        return new audExternalStreamSound(this, hashKey);
                    case dat54_audMetadataTypes.SoundSet:
                        return new audSoundSet(this, hashKey);
                    default:
                        return new audByteArray(this, hashKey);
                }
            }

            else if (Type == RageAudioMetadataFileType.Dat4)
            {
                return new audByteArray(this, hashKey);
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

            foreach (audDataBase dataEntry in DataItems)
            {
                builder.AppendFormat("\n[{0}] {1} @ 0x{2:X} [Length:{3}]\n{4}\n", dataEntry.Name, dataEntry.GetType().Name, dataEntry.FileOffset, dataEntry.Serialize().Length, dataEntry.ToString());
            }

            builder.AppendLine();

            builder.AppendLine("Hash Section Length: " + HashItems.Length);

            builder.AppendLine();

            foreach (var item in HashItems)
            {
                builder.AppendFormat("0x{0:X} (Offset: 0x{1:X})\n", item.Value, item.FileOffset);
            }

            builder.AppendLine();

            builder.AppendLine("Hash Section 1 Length: " + HashItems1.Length);

            builder.AppendLine();

            foreach (var item in HashItems1)
            {
                builder.AppendFormat("0x{0:X} (Offset: 0x{1:X})\n", item.Value, item.FileOffset);
            }

            return builder.ToString();
        }
    }
}
