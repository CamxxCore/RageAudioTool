using System.ComponentModel;
using System.Xml.Serialization;
using RageAudioTool.Interfaces;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public abstract class audDataBase : ISerializable
    {
        protected RageDataFile parent;

        [XmlElement(IsNullable = false)]
        public HashString Name { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int FileOffset { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int Length { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public RageDataFile Parent
        {
            get { return parent; }
        }   

        public abstract byte[] Serialize();

        public abstract int Deserialize(byte[] data);

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="hashName">Data object hash.</param>
        public audDataBase(RageDataFile parent, uint hashName)
        {
            this.parent = parent;

            string str;

            if (parent.Nametable.TryGetValue(hashName, out str))
            {
                Name = str;
            }

            else
                Name = hashName;
        }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="str">Data object name.</param>
        public audDataBase(RageDataFile file, string str)
        {
            parent = file;
            Name = str;
        }

        public audDataBase()
        { }
    }
}
