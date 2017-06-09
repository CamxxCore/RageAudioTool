using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    [TypeConverter(typeof(NamedObjectConverter))]
    public class audHashDesc
    {
        public audHashDesc()
        { }

        public audHashDesc(audHashString trackName, int offset)
        {
            TrackName = trackName;
            Offset = offset;
        }

        [XmlText]
        [Browsable(false)]
        public string Value
        {
            get { return TrackName.ToString(); }
            set { TrackName.HashName = value; }
        }

        [XmlIgnore]
        public audHashString TrackName { get; set; } = new audHashString();

        [XmlIgnore]
        [Browsable(false)]
        public int Offset { get; set; }
    }
}
