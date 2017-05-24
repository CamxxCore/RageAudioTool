using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile;

namespace RageAudioTool.XML
{
    public class ResourceXmlWriter5
    {
        private string _filename;

        public ResourceXmlWriter5(string filename)
        {
            this._filename = filename;
        }

        public void WriteData(RageAudioMetadata5 data)
        {
            using (var writer = XmlWriter.Create(_filename, new XmlWriterSettings() { Indent = true }))
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
