using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.Reflection;

namespace RageAudioTool.Rage_Wrappers.DatFile.XML
{
    class ResourceXMLReader5
    {
        private string filename;

        public ResourceXMLReader5(string fileName)
        {
            filename = fileName;
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

            data.Type = RageAudioMetadataFileType.Dat54_DataEntries;

            List<string> stringTable = new List<string>();

            List<audSoundBase> sounds = new List<audSoundBase>();

            List<audHash> hashItems = new List<audHash>();

            List<audHash> hashItems1 = new List<audHash>();

            using (XmlReader reader = XmlReader.Create(filename))
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
