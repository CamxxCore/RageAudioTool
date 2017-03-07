using System;
using System.Text;
using System.Runtime.CompilerServices;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    

    /// <summary>
    /// Metadata without strings e.g. speech resources, dat54 (usually)
    /// </summary>
    public class RageAudioMetadata5 : RageDataFile
    {
        public audSoundBase[] DataItems
        {
            get { return dataItems; }
            set { dataItems = value; }
        }

        public audHash[] HashItems
        {
            get { return hashItems; }
            set { hashItems = value; }
        }

        public audHash[] HashItems1
        {
            get { return hashItems1; }
            set { hashItems1 = value; }
        }

        private audSoundBase[] dataItems;

        private audHash[] hashItems, hashItems1;

        public override void Read(RageDataFileReadReference file)
        {
            base.Read(file);

            int count = file.ReadInt32();

            dataItems = ReadDataItems(file, count);

            count = file.ReadInt32();

            hashItems = ReadHashItems(file, count);

            count = file.ReadInt32();

            hashItems1 = ReadHashItems(file, count); // read audio bank hashes.
        }

        filePtrPair[] hashIndexes = new filePtrPair[0x400];

        private uint GetEncryptedFilePtr(int offset, uint hash)
        {
            var maskedIdx = hash & 0xFF;

            var item = hashIndexes[maskedIdx];

            var v1 = item.A;

            var v2 = item.B - 1;

            uint it = 0;

            for (int i = 0; i < v2; i++)
            {
                it = (uint)(v2 + i) >> 1;

                var index = v1 + it;

                if (maskedIdx <= (dataItems[index].HashName & 0xFF))
                {
                    break;
                }
            }

            return (it + v1);
        }

        private void getHshIdxItems()
        {
            for (int i = 0; i < dataItems.Length; i++)
            {
                var item = dataItems[i];

                ++hashIndexes[item.HashName & 0xFF].B;
            }

            uint v3 = 0;

            int y = 1;

            for (int x = 0xFF; x != 0; x--)
            {
                v3 += hashIndexes[y - 1].B;

                hashIndexes[y].A = v3;

                y += 1;
            }
        }

        public audHash[] ReadHashItems(RageDataFileReadReference file, int itemCount)
        {
            var items = new audHash[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var offset = file.ReadUInt32();
                offset -= 0x8;

                byte[] data = new byte[0x4];

                Buffer.BlockCopy(DataSection, (int)offset, data, 0, 4);

                uint result = BitConverter.ToUInt32(data, 0);

                /*   var ptr = GetEncryptedFilePtr((int)offset, result);

                   result = (0 << 24) | ptr & 0xFFFFFF;

                   var bytes = BitConverter.GetBytes(result);

                   Buffer.BlockCopy(data, 0, DataSection, (int)offset, 4);

                //   System.Windows.Forms.MessageBox.Show("result: " + result.ToString());*/

                items[i] = new audHash(result);
            }

            return items;
        }

        protected audSoundBase[] ReadDataItems(RageDataFileReadReference file, int itemCount)
        {
            var items = new audSoundBase[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var hashKey = file.ReadUInt32(); // Hash key of this object. For dynamic mixer data, this appears to be the hash for the mixer group (See native AUDIO::_0x153973AB99FE8980)

                var offset = file.ReadInt32(); // Offset pointing to the relevent data in the data section.

                var length = file.ReadInt32(); // Total length of data from data section.

                byte[] data = new byte[length];

                Array.Copy(DataSection, offset, data, 0, length);

                items[i] = CreateDerivedDataType(data[0], hashKey);             

                items[i].Deserialize(data);

                if (NametablePresent && NametableObjects[hashKey] != null)
                {
                    items[i].Name = NametableObjects[hashKey];
                }
            }

            return items;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private audSoundBase CreateDerivedDataType(int dataType, uint hashKey)
        {
            if (Type == RageAudioMetadataFileType.Dat151_Parameters)
            {
                switch ((dat151_audMetadataTypes)dataType)
                {
                    case dat151_audMetadataTypes.ShoreLinePool:
                        return new audShorelinePoolMetadata(hashKey);
                }
            }

            else if (Type == RageAudioMetadataFileType.Dat54_DataEntries)
            {
                switch ((dat54_audMetadataTypes)dataType)
                {
                    case dat54_audMetadataTypes.MultitrackSound:
                        return new audMultitrackSound(hashKey);
                    case dat54_audMetadataTypes.VariableBlockSound:
                        return new audVariableBlockSound(hashKey);
                    case dat54_audMetadataTypes.SimpleSound:
                        return new audSimpleSound(hashKey);
                    case dat54_audMetadataTypes.EnvelopeSound:
                        return new audEnvelopeSound(hashKey);
                    case dat54_audMetadataTypes.TwinLoopSound:
                        return new audTwinLoopSound(hashKey);
                    case dat54_audMetadataTypes.SpeechSound:
                        return new audSpeechSound(hashKey);
                    case dat54_audMetadataTypes.OnStopSound:
                        return new audOnStopSound(hashKey);
                    case dat54_audMetadataTypes.WrapperSound:
                        return new audWrapperSound(hashKey);
                    case dat54_audMetadataTypes.SequentialSound:
                        return new audSequentialSound(hashKey);
                    case dat54_audMetadataTypes.StreamingSound:
                        return new audStreamingSound(hashKey);
                    case dat54_audMetadataTypes.RetriggeredOverlappedSound:
                        return new audRetriggeredOverlappedSound(hashKey);
                    case dat54_audMetadataTypes.CrossfadeSound:
                        return new audCrossfadeSound(hashKey);
                    case dat54_audMetadataTypes.CollapsingStereoSound:
                        return new audCollapsingStereoSound(hashKey);
                    case dat54_audMetadataTypes.RandomizedSound:
                        return new audRandomizedSound(hashKey);
                    case dat54_audMetadataTypes.EnvironmentSound:
                        return new audEnvironmentSound(hashKey);
                    case dat54_audMetadataTypes.DynamicEntitySound:
                        return new audDynamicEntitySound(hashKey);
                    case dat54_audMetadataTypes.SequentialOverlapSound:
                        return new audSequentialOverlapSound(hashKey);
                    case dat54_audMetadataTypes.ModularSynthSound:
                        return new audModularSynthSound(hashKey);
                    case dat54_audMetadataTypes.GranularSound:
                        return new audGranularSound(hashKey);
                    case dat54_audMetadataTypes.DirectionalSound:
                        return new audDirectionalSound(hashKey);
                    case dat54_audMetadataTypes.KineticSound:
                        return new audKineticSound(hashKey);
                    case dat54_audMetadataTypes.SwitchSound:
                        return new audSwitchSound(hashKey);
                    case dat54_audMetadataTypes.VariableCurveSound:
                        return new audVariableCurveSound(hashKey);
                    case dat54_audMetadataTypes.VariablePrintValueSound:
                        return new audVariablePrintValueSound(hashKey);
                    case dat54_audMetadataTypes.IfSound:
                        return new audIfSound(hashKey);
                    case dat54_audMetadataTypes.MathOperationSound:
                        return new audMathOperationSound(hashKey);
                    case dat54_audMetadataTypes.ParameterTransformSound:
                        return new audParameterTransformSound(hashKey);
                    case dat54_audMetadataTypes.FluctuatorSound:
                        return new audFluctuatorSound(hashKey);
                    case dat54_audMetadataTypes.AutomationSound:
                        return new audAutomationSound(hashKey);
                    case dat54_audMetadataTypes.ExternalStreamSound:
                        return new audExternalStreamSound(hashKey);
                    default:
                        return new audUnknownSound(hashKey);
                }
            }

            return null;
        }

        protected void ReadUnkHashItems2(RageDataFileReadReference file, int itemCount)
        {
            hashItems1 = new audHash[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var offset = file.ReadInt32();
                offset -= 0x8;

                byte[] data = new byte[0x4];

                Buffer.BlockCopy(DataSection, offset, data, 0, 4);

                uint result = (uint)BitConverter.ToInt32(data, 0);

                var masked = offset & 0xFFFFFF | (i << 24);

                HashItems[i] = new audHash(result);
            }
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

            for (int i = 0; i < dataItems.Length; i++)
            {
                var dataEntry = dataItems[i];

                builder.AppendFormat("\n{0}: {1}", dataEntry.Name, dataEntry);
            }

            builder.AppendLine();

            builder.AppendLine("Hash Section Length: " + HashItems.Length);

            builder.AppendLine();

            for (int i = 0; i < HashItems.Length; i++)
            {
                builder.AppendFormat("HashItems[{0}]: 0x{1:X}\n", i, HashItems[i].Data);
            }

            builder.AppendLine();

            builder.AppendLine("Hash Section 1 Length: " + HashItems1.Length);

            builder.AppendLine();

            for (int i = 0; i < HashItems1.Length; i++)
            {
                builder.AppendFormat("AudioBankHashes[{0}]: 0x{1:X}\n", i, HashItems1[i].Data);
            }
            return builder.ToString();
        }
    }
}
