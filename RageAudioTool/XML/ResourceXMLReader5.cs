using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile;
using RageAudioTool.Rage_Wrappers.DatFile.Types;
using RageAudioTool.Rage_Wrappers.DatFile.Types.Metadata;

namespace RageAudioTool.XML
{
    class ResourceXmlReader5
    {
        private string _filename;

        public ResourceXmlReader5(string fileName)
        {
            _filename = fileName;
        }

        public audSoundBase GetDerivedDataType(XmlReader reader, string elementName)
        {
            var baseType = typeof(audSoundBase);

            var assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly
                .GetTypes()
                .Where(t => t.IsClass && t.IsSubclassOf(baseType)))
            {
                if (type.Name == elementName)
                {
                 //   System.Windows.Forms.MessageBox.Show("found it");

                    var serializer = new XmlSerializer(type);

                    return (audSoundBase) serializer.Deserialize(reader);
                }
            }

            return null;
        }

        public RageAudioMetadata5 ReadData()
        {
            RageAudioMetadata5 data = new RageAudioMetadata5();

            data.Type = RageAudioMetadataFileType.Dat54DataEntries;

            List<string> stringTable = new List<string>();

            List<audSoundBase> sounds = new List<audSoundBase>();

            List<audHash> hashItems = new List<audHash>();

            List<audHash> hashItems1 = new List<audHash>();

            using (XmlReader reader = XmlReader.Create(_filename))
            {
                reader.ReadToFollowing("stringTable");

                if (reader.ReadToDescendant("item"))
                {
                    while(reader.ReadToNextSibling("item"))
                    {
                        string str = reader.ReadElementContentAsString();

                        stringTable.Add(str);
                    }
                }

                data.StringTable = stringTable.ToArray();

                reader.ReadToFollowing("dataEntries");

                while (reader.Read())
                {
                    if (!reader.IsEmptyElement && reader.NodeType == XmlNodeType.Element && reader.Name != string.Empty)
                    {
                        if (!reader.IsStartElement()) break;

                        var result = GetDerivedDataType(reader, reader.Name);

                        if (result != null)
                        {
                            sounds.Add(result);
                        }
                    }
                }

                data.DataItems = sounds.ToArray();

                reader.ReadToFollowing("hashItems");

                if (reader.ReadToDescendant("item"))
                {
                    while (reader.ReadToNextSibling("item"))
                    {
                        string str = reader.ReadElementContentAsString();

                        hashItems.Add(new audHash(data, Convert.ToUInt32(str)));
                    }
                }

                data.HashItems = hashItems.ToArray();

                reader.ReadToFollowing("hashItems1");

                if (reader.ReadToDescendant("item"))
                {
                    while (reader.ReadToNextSibling("item"))
                    {
                        string str = reader.ReadElementContentAsString();

                        hashItems1.Add(new audHash(data, Convert.ToUInt32(str)));
                    }
                }

                data.HashItems1 = hashItems1.ToArray();
            }

            return data;
        }

        private T Deserialize<T>(XmlReader reader)
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T) serializer.Deserialize(reader);
        }
    }
}
