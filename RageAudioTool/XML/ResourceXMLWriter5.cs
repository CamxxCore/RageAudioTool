using System;
using System.Xml;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile.XML
{
    public class ResourceXMLWriter5
    {
        private string filename;

        public ResourceXMLWriter5(string filename)
        {
            this.filename = filename;
        }

        public void WriteData(RageAudioMetadata5 data)
        {
            using (var writer = XmlWriter.Create(filename, new XmlWriterSettings() { Indent = true }))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement(data.Type.ToString());

                writer.WriteStartElement("stringTable");

                writer.WriteAttributeString("sectionSize", data.StringSectionSize.ToString());

                foreach (var item in data.StringTable)
                {
                    writer.WriteElementString("item", item);
                }

                writer.WriteEndElement();

                writer.WriteStartElement("dataEntries");

                writer.WriteAttributeString("sectionSize", data.DataItems.Length.ToString());

                foreach (var item in data.DataItems)
                {
                    Serialize(writer, item);

                   // writer.WriteWhitespace("\n\n");
                }

                writer.WriteEndElement();

                writer.WriteStartElement("hashItems");

                foreach (var item in data.HashItems)
                {
                    writer.WriteElementString("item", item.Value.ToString());
                }

                writer.WriteEndElement();

                writer.WriteStartElement("hashItems1");

                foreach (var item in data.HashItems1)
                {
                    writer.WriteElementString("item", item.Value.ToString());
                }

                writer.WriteEndElement();

                writer.WriteEndElement();

                writer.WriteEndDocument();
            }
        }

        /// <summary>
        /// Serializes a type via the <see cref="XmlSerializer"/> class.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="o"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]       
        private void Serialize(XmlWriter writer, object o)
        {
            XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();

            namespaces.Add(string.Empty, string.Empty);

            Type type = o.GetType();

            var serializer = new XmlSerializer(type);

            serializer.Serialize(writer, o, namespaces);
        }
    }
}
