using System.Xml;
namespace RageAudioTool.Interfaces
{
    interface IXMLSerializable
    {
        XmlDocument ToXml();
    }
}
