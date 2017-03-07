using System;
using System.Xml;
using System.Xml.Serialization;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    /// <summary>
    /// Represents a data entry in an audio file.
    /// </summary>
    public abstract class audSoundBase : ISerializable<byte[]>
    {
        [XmlIgnore]
        public uint HashName { get; set; }

        [XmlIgnore]
        public byte DataType; //0x0-0x1

        [XmlIgnore]
        public uint DataFlags; //0x1-0x5 

        [XmlElement(IsNullable = false)]
        public string Name
        {
            get
            {
                return name?.Length > 0 ? name : string.Format("0x{0}", HashName.ToString("X"));
            }

            set
            {
                name = value;
            }
        }

        [XmlIgnore]
        protected string name;

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="hashName">Data object hash.</param>
        public audSoundBase(uint hashName)
        {
            HashName = hashName;
        }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="str">Data object name.</param>
        public audSoundBase(string str)
        {
            name = str;

            HashName = Utility.HashKey(name);
        }

        /// <summary>
        /// Default parameterless constructor for serialization.
        /// </summary>
        public audSoundBase()
        { }

        public virtual byte[] Serialize() { return null; }

        public virtual int Deserialize(byte[] data)
        {
            int position = 0;

            DataType = data[position];

            position++;

            DataFlags = BitConverter.ToUInt32(data, position);

            position += 4;

            if ((DataFlags & 1) != 0)
            {
                uint v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                UnkHash = v4;
            }

            if ((DataFlags & 2) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk1 = v4;
            }

            if ((DataFlags & 4) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk2 = v4;
            }

            if ((DataFlags & 8) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk3 = v4;
            }

            if ((DataFlags & 0x10) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk4 = v4;
            }

            if ((DataFlags & 0x20) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk5 = v4;
            }

            if ((DataFlags & 0x40) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk6 = v4;
            }

            if ((DataFlags & 0x80) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk7 = v4;
            }

            if ((DataFlags & 0x100) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk8 = v4;
            }

            if ((DataFlags & 0x200) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk9 = v4;
            }
            if ((DataFlags & 0x400) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                UnkHash1 = v4;
            }

            if ((DataFlags & 0x800) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                UnkHash2 = v4;
            }

            if ((DataFlags & 0x1000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk10 = v4;
            }

            if ((DataFlags & 0x2000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk11 = v4;
            }

            if ((DataFlags & 0x4000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk12 = v4;
            }

            if ((DataFlags & 0x8000) != 0) // category hash 0x29
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                CategoryHash = v4;
            }

            if ((DataFlags & 0x10000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk14 = v4;
            }

            if ((DataFlags & 0x20000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk15 = v4;
            }

            if ((DataFlags & 0x40000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk16 = v4;
            }

            if ((DataFlags & 0x80000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk17 = v4;
            }

            if ((DataFlags & 0x100000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                UnkHash3 = v4;
            }

            if ((DataFlags & 0x200000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk18 = v4;
            }

            if ((DataFlags & 0x400000) != 0)
            {
                var v4 = BitConverter.ToChar(data, position);
                position++;

                Unk19 = (byte)v4;
            }

            if ((DataFlags & 0x800000) != 0)
            {
                var v4 = BitConverter.ToChar(data, position);
                position++;

                Unk20 = (byte)v4;
            }

            if ((DataFlags & 0x1000000) != 0)
            {
                var v4 = BitConverter.ToChar(data, position);
                position++;

                Unk21 = (byte)v4;
            }

            if ((DataFlags & 0x2000000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                UnkHash4 = v4;
            }

            if ((DataFlags & 0x4000000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                UnkHash5 = v4;
            }

            if ((DataFlags & 0x8000000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk22 = v4;
            }

            if ((DataFlags & 0x10000000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk23 = v4;
            }

            if ((DataFlags & 0x20000000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                Unk24 = v4;
            }

            return position;
        }

        #region Common Data

        [XmlElement]
        public uint UnkHash; //0x5-0x9

        [XmlElement]
        public ushort Unk1; //0x9-0xB

        [XmlElement]
        public ushort Unk2; //0xB-0xD

        [XmlElement]
        public ushort Unk3; //0xD-0xF

        [XmlElement]
        public ushort Unk4; //0xF-0x11

        [XmlElement]
        public ushort Unk5; //0x11-0x13

        [XmlElement]
        public ushort Unk6; //0x13-0x15

        [XmlElement]
        public ushort Unk7; //0x15-0x17

        [XmlElement]
        public ushort Unk8; //0x17-0x19

        [XmlElement]
        public ushort Unk9; //0x19-0x1B

        [XmlElement]
        public uint UnkHash1; //0x1B-0x1F

        [XmlElement]
        public uint UnkHash2; //0x1F-0x23

        [XmlElement]
        public ushort Unk10; //0x23-0x25

        [XmlElement]
        public ushort Unk11; //0x25-0x27

        [XmlElement]
        public ushort Unk12; //0x27-0x29

        [XmlElement]
        public uint CategoryHash; //0x29-0x2D

        [XmlElement]
        public ushort Unk14; //0x2D-0x2F

        [XmlElement]
        public ushort Unk15; //0x2F-0x31

        [XmlElement]
        public ushort Unk16; //0x31-0x33

        [XmlElement]
        public ushort Unk17; //0x33-0x35

        [XmlElement]
        public uint UnkHash3; //0x35-0x39

        [XmlElement]
        public ushort Unk18; //0x39-0x3B

        [XmlElement]
        public byte Unk19; //0x3B-0x3C

        [XmlElement]
        public byte Unk20; //0x3C-0x3D

        [XmlElement]
        public byte Unk21; //0x3D-0x3E

        [XmlElement]
        public uint UnkHash4; //0x3E-0x42

        [XmlElement]
        public uint UnkHash5; //0x42-0x46

        [XmlElement]
        public ushort Unk22; //0x46-0x48

        [XmlElement]
        public ushort Unk23; //0x48-0x4A

        [XmlElement]
        public ushort Unk24; //0x4A-0x4C

        #endregion
    }

   /* public abstract class audSoundBase<T> : audSoundBase
    {
        public virtual T Data { get; set; }

        public override int Deserialize(byte[] data)
        {
            return base.Deserialize(data);
        }

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="hashName">Data object hash.</param>
        public audSoundBase(uint hashName)
        {
            HashName = hashName;
        }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="str">Data object name.</param>
        public audSoundBase(string str)
        {
            name = str;

            HashName = Utility.HashKey(name);
        }

        /// <summary>
        /// Default parameterless constructor for serialization.
        /// </summary>
        public audSoundBase()
        { }
    }*/
}
