using System.ComponentModel;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    /// <summary>
    /// Represents a data entry in an audio file.
    /// </summary>
    /// 
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class audSoundBase : audDataBase
    {
        [XmlIgnore]
        [Browsable(false)]
        public int DataOffset { get; protected set; }

        [ReadOnly(false)]
        public audSoundHeader Header { get; set; }

        [ReadOnly(false)]
        public audHashCollection AudioTracks { get; }

        [ReadOnly(false)]
        public audHashCollection AudioContainers { get; set; }

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="hashName">Data object hash.</param>
        protected audSoundBase(RageDataFile parent, uint hashName) : base(parent, hashName)
        {
            Header = new audSoundHeader(parent);
            AudioTracks = new audHashCollection(this);
            AudioContainers = new audHashCollection(this);          
        }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="str">Data object name.</param>
        protected audSoundBase(RageDataFile parent, string str) : base(parent, str)
        {
            Header = new audSoundHeader(parent);
            AudioTracks = new audHashCollection(this);
            AudioContainers = new audHashCollection(this);        
        }

        /// <summary>
        /// Default parameterless constructor for serialization.
        /// </summary>
        protected audSoundBase()
        {
            Header = new audSoundHeader();
            AudioTracks = new audHashCollection(this);
            AudioContainers = new audHashCollection(this);           
        }

      //  public void AddTrack(audHashDesc hash)
      //  {
      //      AudioTracks.Add(hash);
      //  }

      //  public void AddContainer(audHashDesc hash)
      //  {
      //      AudioContainers.Add(hash);
      //  }

        public override byte[] Serialize()
        {
            var data = Header.Serialize();

            DataOffset = data.Length;

            return data;
        }

        public override int Deserialize(byte[] data)
        {
            AudioTracks.Clear();
            return DataOffset = Header.Deserialize(data);
        }
    }
}
