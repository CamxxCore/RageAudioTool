using System;
using System.Text;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    /// <summary>
    /// Metadata with strings e.g. config files
    /// </summary>
    public class RageAudioMetadata4 : RageDataFile
    {
        public IAudFiletype[] DataItems { get { return dataItems; } }

        private IAudFiletype[] dataItems;

        public override void Read(RageDataFileReadReference file)
        {
            base.Read(file);

            int count = file.ReadInt32();

            int nametableLength = file.ReadInt32();

            dataItems = ReadDataItems(file, count);

            count = file.ReadInt32();
        }

        protected unsafe IAudFiletype[] ReadDataItems(RageDataFileReadReference file, int itemCount)
        {
            var items = new IAudFiletype[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var variableName = file.ReadString();

                var offset = file.ReadInt32();

                var dataSize = file.ReadInt32();

                dataSize -= 8; // dataSize - sizeof first two items.

                var maskedType = BitConverter.ToUInt32(DataSection, offset);

                int dataType = (int)(maskedType ^= maskedType & 0xFFFFFF00);

                offset += 8; // skip first two items

                byte[] data = new byte[dataSize]; // read item data

                Buffer.BlockCopy(DataSection, offset, data, 0, dataSize);

                switch (dataType)
                {
                    case 1:
                        items[i] = new audInteger(variableName);
                        break;
                    case 2:
                        items[i] = new audFloat(variableName);
                        break;
                    case 3:
                        items[i] = new audString(variableName);
                        break;
                    case 5:
                        items[i] = new audVector(variableName);
                        break;
                    case 10:
                        items[i] = new audFloatArray(variableName);
                        break;

                    default:
                        items[i] = new audByteArray(variableName);
                        break;
                }

                items[i].Deserialize(data);
            }

            return items;
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
