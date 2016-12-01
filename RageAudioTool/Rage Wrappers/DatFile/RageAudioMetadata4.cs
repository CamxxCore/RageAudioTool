using System;
using System.Text;
using RageAudioTool.Types;
using RageAudioTool.IO;
using System.Linq;
using System.Reflection;
namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class RageAudioMetadata4 : RageAudioMetadata
    {
        public IRageAudioStringPair[] DataItems { get; private set; }

        public IRageAudioDatBase[] HashItems { get; private set; }

        public RageAudioMetadata4(byte[] bytes) : base(new IOFileReader(bytes, Encoding.GetEncoding(1252)))
        {
            if (Type != RageAudioMetadataFileType.Dat4_AudioConfig) return;

            int count = Reader.ReadInt32();

            int nametableLength = Reader.ReadInt32();

            ReadDataItems(count);

            count = Reader.ReadInt32();

            ReadHashItems(count);
        }

        protected unsafe void ReadDataItems(int itemCount)
        {
            DataItems = new IRageAudioStringPair[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var strLen = Reader.ReadByte();

                var variableName = Reader.ReadString(strLen);

                var offset = Reader.ReadInt32();

                var dataSize = Reader.ReadInt32();

                dataSize -= 8; // dataSize - sizeof first two 32bit items.

                uint dataType = (uint) BitConverter.ToInt32(DataSection, offset);

                dataType ^= dataType & 0xFFFFFF00;

                offset += 8; // skip first two items

                byte[] data = new byte[dataSize]; //read item data

                Buffer.BlockCopy(DataSection, offset, data, 0, dataSize);

                switch (dataType)
                {
                    case 1:
                        DataItems[i] = new DatTypeStringPair<DatTypeInt32>(variableName, new DatTypeInt32(BitConverter.ToInt32(data, 0)));
                        goto setnametmp;

                    case 2:
                        DataItems[i] = new DatTypeStringPair<DatTypeFloat>(variableName, new DatTypeFloat(BitConverter.ToSingle(data, 0)));
                        goto setnametmp;

                    case 3:
                        DataItems[i] = new DatTypeStringPair<string>(variableName, new DatTypeString(Encoding.ASCII.GetString(data.TakeWhile(b => !b.Equals(0)).ToArray())));
                        goto setnametmp;

                    case 5:
                        fixed (byte* ptr = &DataSection[offset]) DataItems[i] = new DatTypeStringPair<DatTypeVector3>(variableName, new DatTypeVector3(*(RageVector3*)(ptr)));
                        goto setnametmp;

                    case 10:
                        DataItems[i] = new DatTypeStringPair<DatTypeFloatArr>(variableName, new DatTypeFloatArr(Enumerable.Repeat(typeof(float), dataSize / 4).Select(x => BitConverter.ToSingle(DataSection, offset += 4)).ToArray()));
                        goto setnametmp;

                        setnametmp:

                        DataItems[i].Name = 
                            string.Format("[Offset: 0x{0}] [{1}] {2}", (offset + 0x10).ToString("x"), DataItems[i].Data.GetType().Name, DataItems[i].Name);
                        break;

                    default:
                        DataItems[i] = new DatTypeStringPair<DatTypeString>(
                            string.Format("[Offset: 0x{0}] [Auto:{1}] {2}", (offset + 0x10).ToString("x"), dataType, variableName), new DatTypeString(BitConverter.ToString(data).Replace("-", "")));
                        break;
                }
            }
        }

        protected void ReadHashItems(int itemCount)
        {
            HashItems = new IRageAudioDatBase[itemCount];

            for (int i = 0; i < itemCount; i++)
            {
                var offset = Reader.ReadInt32();
                offset -= 0x8;

                byte[] data = new byte[0x4];

                Buffer.BlockCopy(DataSection, offset, data, 0, 4);

                uint result = (uint)BitConverter.ToInt32(data, 0);

                var masked = offset & 0xFFFFFF | (i << 24);

                HashItems[i] = new DatTypeInt32(data);
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
                builder.AppendFormat("{0}= {1}\n", dataEntry.Name, dataEntry.Data);

            builder.AppendLine();

            builder.AppendLine("Wave Section Length: " + HashItems.Length);

            builder.AppendLine();

            for (int i = 0; i < HashItems.Length; i++)
            {
                builder.AppendFormat("WaveSlotsList[{0}]: 0x{1:X}\n", i, HashItems[i].Data);
            }

            return builder.ToString();
        }

        public byte[] ToByteArray()
        {
            return new byte[] { };
        }
    }
}
