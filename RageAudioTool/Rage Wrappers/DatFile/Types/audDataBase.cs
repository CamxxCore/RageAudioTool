using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.Interfaces;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public abstract class audDataBase : ISerializable, IRageAudioFiletype
    {
        protected RageDataFile parent;

        [Description("Name of the sound")]
        [XmlElement(IsNullable = false)]
        public audHashString Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int FileOffset { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int Length { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public RageDataFile Parent => parent;

        public abstract byte[] Serialize();

        public abstract int Deserialize(byte[] data);

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="hashName">Data object hash.</param>
        public audDataBase(RageDataFile file, uint hashName)
        {
            parent = file;
            Name = new audHashString(file, hashName);
        }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="str">Data object name.</param>
        public audDataBase(RageDataFile file, string str)
        {
            parent = file;
            Name = new audHashString(file, str);
        }

        public audDataBase()
        { }
    }
}
