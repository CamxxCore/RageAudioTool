using System.Xml;

namespace RageAudioTool.Interfaces
{
    interface IXmlSerializable
    {
        XmlDocument ToXml();
    }
}
