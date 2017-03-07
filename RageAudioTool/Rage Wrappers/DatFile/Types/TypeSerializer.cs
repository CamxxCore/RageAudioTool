using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace RageAudioTool.Rage_Wrappers.DatFile.Types
{
    public class XMLFileWriter
    {
        private string filename;

        private XmlWriter baseWriter;

        public XmlWriter Base {  get { return baseWriter; } }

        public XMLFileWriter(string filename)
        {
            baseWriter = XmlWriter.Create(filename);
        }

        public XMLFileWriter(Stream stream)
        {
            baseWriter = XmlWriter.Create(stream);
        }

        public void WriteObject<T>(T obj)
        {
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(baseWriter, obj);
        }
    }
}
