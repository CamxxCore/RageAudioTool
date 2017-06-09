using System;
using System.Runtime.CompilerServices;
using System.Xml;
using System.Xml.Serialization;
using RageAudioTool.Rage_Wrappers.DatFile;

namespace RageAudioTool.XML
{
    public class ResourceXmlWriter5
    {
        private readonly string _filename;

        public ResourceXmlWriter5(string filename)
        {
            _filename = filename;
        }

        public void WriteData(RageAudioMetadata5 data)
        {
            using (var writer = XmlWriter.Create(_filename, 
                new XmlWriterSettings() { Indent = true, WriteEndDocumentOnClose = true }))
            {
                writer.WriteStartDocument();

                writer.WriteStartElement(data.Type.ToString());

                writer.WriteStartElement("waveContainers");

                foreach (var item in data.StringTable)
                {
                    writer.WriteElementString("item", item);
                }

                writer.WriteEndElement();

                writer.WriteStartElement("dataEntries");

                foreach (var item in data.DataItems)
                {
                    Serialize(writer, item);

                   // writer.WriteWhitespace("\n\n");
                }

                writer.WriteEndElement();
/*
                writer.WriteStartElement("waveTracks");

                foreach (var item in data.WaveTracks)
                {
                    writer.WriteElementString("item", item.Value.ToString());
                }

                writer.WriteEndElement();*/

          /*      writer.WriteStartElement("waveContainers");

                foreach (var item in data.WaveContainers)
                {
                    writer.WriteElementString("item", item.Value.ToString());
                }

                writer.WriteEndElement();
                
    */

                writer.WriteEndElement();

                writer.Close();
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
