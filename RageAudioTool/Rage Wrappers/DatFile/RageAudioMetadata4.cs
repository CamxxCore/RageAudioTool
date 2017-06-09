using System;
using System.Text;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    /// <summary>
    /// Metadata with strings e.g. config files
    /// </summary>
    public class RageAudioMetadata4 : RageDataFile
    {
        public int NametableLength { get; set; }

        public override audDataBase[] ReadDataItems(RageDataFileReadReference file, int itemCount)
        {
            NametableLength = file.ReadInt32();

            var items = new audDataBase[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var variableName = file.ReadString();

                var offset = file.ReadInt32();

                var length = file.ReadInt32();

                int dataType = DataSection[offset];

                byte[] data = new byte[length]; // read item data

                Buffer.BlockCopy(DataSection, offset, data, 0, length);

                switch (dataType)
                {
                    case 0:
                        items[i] = new audInteger(this, variableName);
                        break;
                    case 1:
                        items[i] = new audUInt(this, variableName);
                        break;
                    case 2:
                        items[i] = new audFloat(this, variableName);
                        break;
                    case 3:
                        items[i] = new audString(this, variableName);
                        break;
                    case 5:
                        items[i] = new audVector(this, variableName);
                        break;
                    case 7:
                        items[i] = new audVariableList(this, variableName);
                        break;
                    case 10:
                        items[i] = new audFloatArray(this, variableName);
                        break;
                    default:
                        items[i] = new audByteArray(this, variableName);
                        break;
                }

                items[i].FileOffset = offset;

                items[i].Length = length;

                items[i].Deserialize(data);
            }

            return items;
        }

        protected override void WriteDataOffsets(RageDataFileWriteReference file)
        {
            file.Write(NametableLength);

            for (int i = 0; i < DataItems.Length; i++)
            {
                file.Write((uint)DataItems[i].Name);

                file.Write(DataItems[i].FileOffset);

                file.Write(DataItems[i].Serialize().Length);
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

                foreach (string str in StringTable)
                    builder.AppendLine(str);
            }

            builder.AppendLine("\n");

            builder.AppendLine("Data Entries Count: " + DataItems.Length);

            builder.AppendLine();

            foreach (var dataEntry in DataItems)
                builder.AppendFormat("{0}= {1}\n", dataEntry.Name, dataEntry);

            builder.AppendLine();

          /*  builder.AppendLine("Wave Section Length: " + HashItems.Length);

            builder.AppendLine();

            for (int i = 0; i < HashItems.Length; i++)
            {
                builder.AppendFormat("WaveSlotsList[{0}]: 0x{1:X}\n", i, HashItems[i].Data);
            }
            */

            return builder.ToString();
        }

        public byte[] ToByteArray()
        {
            return new byte[] { };
        }
    }
}
