using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile;

namespace RageAudioTool.XML
{
    class ResourceXmlReader5
    {
        private readonly string _filename;

        public ResourceXmlReader5(string fileName)
        {
            _filename = fileName;
        }

        public RageAudioMetadata5 ReadData()
        {
            RageAudioMetadata5 data = new RageAudioMetadata5
                {
                    Type = RageAudioMetadataFileType.Dat54DataEntries
                };

            List<string> stringTable = new List<string>();

            List<audSoundBase> sounds = new List<audSoundBase>();

            using (XmlReader reader = XmlReader.Create(_filename))
            {
                reader.ReadToFollowing("waveContainers");

                if (reader.ReadToDescendant("item"))
                {
                    string str = reader.ReadElementContentAsString();

                    stringTable.Add(str);

                    while (reader.ReadToNextSibling("item"))
                    {
                        str = reader.ReadElementContentAsString();

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

                        var result = DeserializeSound(reader, reader.Name);

                        if (result != null)
                        {
                            sounds.Add(result);
                        }
                    }
                }
 
                data.DataItems = sounds.ToArray();
            }

            return data;
        }

        public audSoundBase DeserializeSound(XmlReader reader, string soundName)
        {
            var baseType = typeof(audSoundBase);

            var assembly = Assembly.GetExecutingAssembly();

            foreach (Type type in assembly
                .GetTypes()
                .Where(t => t.IsSubclassOf(baseType)))
            {
                if (type.Name == soundName)
                {
                    var serializer = new XmlSerializer(type);

                    return (audSoundBase)serializer.Deserialize(reader);
                }
            }

            return null;
        }
    }
}
