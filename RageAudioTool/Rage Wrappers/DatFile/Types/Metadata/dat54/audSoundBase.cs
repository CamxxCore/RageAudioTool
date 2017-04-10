using System;
using System.Xml;
using System.Xml.Serialization;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    /// <summary>
    /// Represents a data entry in an audio file.
    /// </summary>
    public abstract class audSoundBase : audDataBase
    {
        [XmlIgnore]
        public byte DataType; //0x0-0x1

        [XmlIgnore]
        public uint DataFlags; //0x1-0x5 

        private audSoundHeader header;

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="hashName">Data object hash.</param>
        public audSoundBase(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="str">Data object name.</param>
        public audSoundBase(RageDataFile parent, string str) : base(parent, str)
        { }

        /// <summary>
        /// Default parameterless constructor for serialization.
        /// </summary>
        public audSoundBase()
        { }

        public override byte[] Serialize()
        {
            return header.Serialize();
        }

        public override int Deserialize(byte[] data)
        {
            return header.Deserialize(data);
        }

        #region Header Data

        [XmlElement]
        public uint UnkHash
        {
            get { return header.UnkHash; }

            set
            {
                header.UnkHash = value;
            }
        }

        [XmlElement]
        public ushort Unk1 {
            get { return header.Unk1; }

            set
            {
                header.Unk1 = value;
            }
        }

        [XmlElement]
        public ushort Unk2
        {
            get { return header.Unk2; }

            set
            {
                header.Unk2 = value;
            }
        }

        [XmlElement]
        public ushort Unk3
        {
            get { return header.Unk3; }

            set
            {
                header.Unk3 = value;
            }
        }

        [XmlElement]
        public ushort Unk4
        {
            get { return header.Unk4; }

            set
            {
                header.Unk4 = value;
            }
        }

        [XmlElement]
        public ushort Unk5
        {
            get { return header.Unk5; }

            set
            {
                header.Unk5 = value;
            }
        }

        [XmlElement]
        public ushort Unk6
        {
            get { return header.Unk6; }

            set
            {
                header.Unk6 = value;
            }
        }

        [XmlElement]
        public ushort Unk7
        {
            get { return header.Unk7; }

            set
            {
                header.Unk7 = value;
            }
        }

        [XmlElement]
        public ushort Unk8
        {
            get { return header.Unk8; }

            set
            {
                header.Unk8 = value;
            }
        }

        [XmlElement]
        public ushort Unk9
        {
            get { return header.Unk9; }

            set
            {
                header.Unk9 = value;
            }
        }

        [XmlElement]
        public uint UnkHash1
        {
            get { return header.UnkHash1; }

            set
            {
                header.UnkHash1 = value;
            }
        }

        [XmlElement]
        public uint UnkHash2
        {
            get { return header.UnkHash2; }

            set
            {
                header.UnkHash2 = value;
            }
        }

        [XmlElement]
        public ushort Unk10
        {
            get { return header.Unk10; }

            set
            {
                header.Unk10 = value;
            }
        }

        [XmlElement]
        public ushort Unk11
        {
            get { return header.Unk11; }

            set
            {
                header.Unk11 = value;
            }
        }

        [XmlElement]
        public ushort Unk12
        {
            get { return header.Unk12; }

            set
            {
                header.Unk12 = value;
            }
        }

        [XmlElement(IsNullable = false)]
        public HashString CategoryName
        {
            get {
                string result;

                if (parent.Nametable.TryGetValue(header.CategoryHash, out result))
                {
                    return result;
                }

                else return header.CategoryHash;
            }

            set
            {
                header.CategoryHash = value.HashKey;
            }
        }

        [XmlElement]
        public ushort Unk14
        {
            get { return header.Unk14; }

            set
            {
                header.Unk14 = value;
            }
        }

        [XmlElement]
        public ushort Unk15
        {
            get { return header.Unk15; }

            set
            {
                header.Unk15 = value;
            }
        }

        [XmlElement]
        public ushort Unk16
        {
            get { return header.Unk16; }

            set
            {
                header.Unk16 = value;
            }
        }

        [XmlElement]
        public ushort Unk17
        {
            get { return header.Unk17; }

            set
            {
                header.Unk17 = value;
            }
        }

        [XmlElement]
        public uint UnkHash3
        {
            get { return header.UnkHash3; }

            set
            {
                header.UnkHash3 = value;
            }
        }

        [XmlElement]
        public ushort Unk18
        {
            get { return header.Unk18; }

            set
            {
                header.Unk18 = value;
            }
        }

        [XmlElement]
        public byte Unk19
        {
            get { return header.Unk19; }

            set
            {
                header.Unk19 = value;
            }
        }

        [XmlElement]
        public byte Unk20
        {
            get { return header.Unk20; }

            set
            {
                header.Unk20 = value;
            }
        }

        [XmlElement]
        public byte Unk21
        {
            get { return header.Unk21; }

            set
            {
                header.Unk21 = value;
            }
        }

        [XmlElement]
        public uint UnkHash4
        {
            get { return header.UnkHash4; }

            set
            {
                header.UnkHash4 = value;
            }
        }

        [XmlElement]
        public uint UnkHash5
        {
            get { return header.UnkHash5; }

            set
            {
                header.UnkHash5 = value;
            }
        }

        [XmlElement]
        public ushort Unk22
        {
            get { return header.Unk22; }

            set
            {
                header.Unk22 = value;
            }
        }

        [XmlElement]
        public ushort Unk23
        {
            get { return header.Unk23; }

            set
            {
                header.Unk23 = value;
            }
        }

        [XmlElement]
        public ushort Unk24
        {
            get { return header.Unk24; }

            set
            {
                header.Unk24 = value;
            }
        }

        #endregion
    }
}
