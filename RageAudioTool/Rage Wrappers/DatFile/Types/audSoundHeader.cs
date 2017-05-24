using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.IO;
using RageAudioTool.Types;
using RageAudioTool.Interfaces;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    [TypeConverter(typeof(NamedObjectConverter))]
    public class audSoundHeader : ISerializable
    {
        public byte DataType;

        public uint DataFlags;

        public uint UnkFlags
        {
            get { return _unkFlags; }

            set
            {
                _unkFlags = value;
                //   if (value.Active)
                //  DataFlags |= 1;
                //  else DataFlags ^= 1;
            }
        }
        private uint _unkFlags;

        public ushort Unk1
        {
            get { return _unk1; }

            set
            {
                _unk1 = value;
                //  DataFlags |= 2;
            }
        }
        private ushort _unk1;

        public ushort Unk2
        {
            get { return _unk2; }
            set
            {
                _unk2 = value;
                //   DataFlags |= 4;
            }
        }
        private ushort _unk2;

        public ushort Unk3
        {
            get { return _unk3; }
            set
            {
                _unk3 = value;
                //  DataFlags |= 8;
            }
        }
        private ushort _unk3; //0xD-0xF

        public ushort Unk4
        {
            get { return _unk4; }
            set
            {
                _unk4 = value;
                //  DataFlags |= 0x10;
            }
        }
        private ushort _unk4; //0xF-0x11

        public ushort Unk5
        {
            get { return _unk5; }
            set
            {
                _unk5 = value;
                //  DataFlags |= 0x20;
            }
        }
        private ushort _unk5; //0x11-0x13

        public ushort Unk6
        {
            get { return _unk6; }
            set
            {
                _unk6 = value;
                //  DataFlags |= 0x40;
            }
        }
        private ushort _unk6; //0x13-0x15

        public ushort Unk7
        {
            get { return _unk7; }
            set
            {
                _unk7 = value;
                //  DataFlags |= 0x80;
            }
        }
        private ushort _unk7; //0x15-0x17

        public ushort Unk8
        {
            get { return _unk8; }
            set
            {
                _unk8 = value;
                //  DataFlags |= 0x100;
            }
        }
        private ushort _unk8; //0x17-0x19

        public ushort Unk9
        {
            get { return _unk9; }
            set
            {
                _unk9 = value;
                //  DataFlags |= 0x200;
            }
        }
        private ushort _unk9; //0x19-0x1B

        public uint UnkHash1
        {
            get { return _unkHash1; }
            set
            {
                _unkHash1 = value;
                // DataFlags |= 0x400;
            }
        }
        private uint _unkHash1; //0x1B-0x1F

        public uint UnkHash2
        {
            get { return _unkHash2; }
            set
            {
                _unkHash2 = value;
                // DataFlags |= 0x800;
            }
        }
        private uint _unkHash2; //0x1F-0x23

        public ushort Unk10
        {
            get { return _unk10; }
            set
            {
                _unk10 = value;
                DataFlags |= 0x1000;
            }
        }
        private ushort _unk10; //0x23-0x25

        public ushort Unk11
        {
            get { return _unk11; }
            set
            {
                _unk11 = value;
                //  DataFlags |= 0x2000;
            }
        }
        private ushort _unk11; //0x25-0x27

        public ushort Unk12
        {
            get { return _unk12; }
            set
            {
                _unk12 = value;
                //  DataFlags |= 0x4000;
            }
        }
        private ushort _unk12; //0x27-0x29

        public audHashString CategoryHash
        {
            get { return new audHashString(_parent, _categoryHash); }
            set
            {
                _categoryHash = value;
                //  DataFlags |= 0x8000;
            }
        }
        private uint _categoryHash; //0x29-0x2D //

        public ushort Unk14
        {
            get { return _unk14; }
            set
            {
                _unk14 = value;
                //  DataFlags |= 0x10000;
            }
        }
        private ushort _unk14; //0x2D-0x2F

        public ushort Unk15
        {
            get { return _unk15; }
            set
            {
                _unk15 = value;
                //   DataFlags |= 0x20000;
            }
        }
        private ushort _unk15; //0x2F-0x31

        public ushort Unk16
        {
            get { return _unk16; }
            set
            {
                _unk16 = value;
                //   DataFlags |= 0x40000;
            }
        }
        private ushort _unk16; //0x31-0x33

        public ushort Unk17
        {
            get { return _unk17; }
            set
            {
                _unk17 = value;
                //  DataFlags |= 0x80000;
            }
        }
        private ushort _unk17; //0x33-0x35

        public uint UnkHash3
        {
            get { return _unkHash3; }
            set
            {
                _unkHash3 = value;
                //  DataFlags |= 0x100000;
            }
        }
        private uint _unkHash3; //0x35-0x39

        public ushort Unk18
        {
            get { return _unk18; }
            set
            {
                _unk18 = value;
                //   DataFlags |= 0x200000;
            }
        }
        private ushort _unk18; //0x39-0x3B

        public byte Unk19
        {
            get { return _unk19; }
            set
            {
                _unk19 = value;
                //  DataFlags |= 0x400000;
            }
        }
        private byte _unk19; //0x3B-0x3C

        public byte Unk20
        {
            get { return _unk20; }
            set
            {
                _unk20 = value;
                //  DataFlags |= 0x800000;
            }
        }
        private byte _unk20; //0x3C-0x3D

        public byte Unk21
        {
            get { return _unk21; }
            set
            {
                _unk21 = value;
                //  DataFlags |= 0x1000000;
            }
        }
        private byte _unk21; //0x3D-0x3E

        public uint UnkHash4
        {
            get { return _unkHash4; }
            set
            {
                _unkHash4 = value;
                //   DataFlags |= 0x2000000;
            }
        }
        private uint _unkHash4; //0x3E-0x42

        public uint UnkHash5
        {
            get { return _unkHash5; }
            set
            {
                _unkHash5 = value;
                //   DataFlags |= 0x4000000;
            }
        }
        private uint _unkHash5; //0x42-0x46

        public ushort Unk22
        {
            get { return _unk22; }
            set
            {
                _unk22 = value;
                //   DataFlags |= 0x8000000;
            }
        }
        private ushort _unk22; //0x46-0x48

        public ushort Unk23
        {
            get { return _unk23; }
            set
            {
                _unk23 = value;
                //  DataFlags |= 0x10000000;
            }
        }
        private ushort _unk23; //0x48-0x4A

        public ushort Unk24
        {
            get { return _unk24; }
            set
            {
                _unk24 = value;
                //  DataFlags |= 0x12000000;
            }
        }
        private ushort _unk24; //0x4A-0x4C

        RageDataFile _parent;

        public audSoundHeader(RageDataFile parent)
        {
            _parent = parent;
        }

        public byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(DataType);

                    writer.Write(DataFlags);

                    if (DataFlags != 0xAAAAAAAA)
                    {
                        if ((DataFlags & 1) != 0)
                        {
                            writer.Write(_unkFlags);
                        }

                        if ((DataFlags & 2) != 0)
                        {
                            writer.Write(_unk1);
                        }

                        if ((DataFlags & 4) != 0)
                        {
                            writer.Write(_unk2);
                        }

                        if ((DataFlags & 8) != 0)
                        {
                            writer.Write(_unk3);
                        }

                        if ((DataFlags & 0x10) != 0)
                        {
                            writer.Write(_unk4);
                        }

                        if ((DataFlags & 0x20) != 0)
                        {
                            writer.Write(_unk5);
                        }

                        if ((DataFlags & 0x40) != 0)
                        {
                            writer.Write(_unk6);
                        }

                        if ((DataFlags & 0x80) != 0)
                        {
                            writer.Write(_unk7);
                        }

                        if ((DataFlags & 0x100) != 0)
                        {
                            writer.Write(_unk8);
                        }

                        if ((DataFlags & 0x200) != 0)
                        {
                            writer.Write(_unk9);
                        }
                        if ((DataFlags & 0x400) != 0)
                        {
                            writer.Write(_unkHash1);
                        }

                        if ((DataFlags & 0x800) != 0)
                        {
                            writer.Write(_unkHash2);
                        }

                        if ((DataFlags & 0x1000) != 0)
                        {
                            writer.Write(_unk10);
                        }

                        if ((DataFlags & 0x2000) != 0)
                        {
                            writer.Write(_unk11);
                        }

                        if ((DataFlags & 0x4000) != 0)
                        {
                            writer.Write(_unk12);
                        }

                        if ((DataFlags & 0x8000) != 0)
                        {
                            writer.Write(_categoryHash);
                        }

                        if ((DataFlags & 0x10000) != 0)
                        {
                            writer.Write(_unk14);
                        }

                        if ((DataFlags & 0x20000) != 0)
                        {
                            writer.Write(_unk15);
                        }

                        if ((DataFlags & 0x40000) != 0)
                        {
                            writer.Write(_unk16);
                        }

                        if ((DataFlags & 0x80000) != 0)
                        {
                            writer.Write(_unk17);
                        }

                        if ((DataFlags & 0x100000) != 0)
                        {
                            writer.Write(_unkHash3);
                        }

                        if ((DataFlags & 0x200000) != 0)
                        {
                            writer.Write(_unk18);
                        }

                        if ((DataFlags & 0x400000) != 0)
                        {
                            writer.Write(_unk19);
                        }

                        if ((DataFlags & 0x800000) != 0)
                        {
                            writer.Write(_unk20);
                        }

                        if ((DataFlags & 0x1000000) != 0)
                        {
                            writer.Write(_unk21);
                        }

                        if ((DataFlags & 0x2000000) != 0)
                        {
                            writer.Write(_unkHash4);
                        }

                        if ((DataFlags & 0x4000000) != 0)
                        {
                            writer.Write(_unkHash5);
                        }

                        if ((DataFlags & 0x8000000) != 0)
                        {
                            writer.Write(_unk22);
                        }

                        if ((DataFlags & 0x10000000) != 0)
                        {
                            writer.Write(_unk23);
                        }

                        if ((DataFlags & 0x20000000) != 0)
                        {
                            writer.Write(_unk24);
                        }
                    }
                }

                return stream.ToArray();
            }
        }

        public int Deserialize(byte[] data)
        {
            int position = 0;

            DataType = data[position];

            position++;

            DataFlags = BitConverter.ToUInt32(data, position);

            position += 4;

            if (DataFlags != 0xAAAAAAAA)
            {
                if ((DataFlags & 1) != 0)
                {
                    uint v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _unkFlags = v4;
                }

                if ((DataFlags & 2) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk1 = v4;
                }

                if ((DataFlags & 4) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk2 = v4;
                }

                if ((DataFlags & 8) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk3 = v4;
                }

                if ((DataFlags & 0x10) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk4 = v4;
                }

                if ((DataFlags & 0x20) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk5 = v4;
                }

                if ((DataFlags & 0x40) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk6 = v4;
                }

                if ((DataFlags & 0x80) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk7 = v4;
                }

                if ((DataFlags & 0x100) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk8 = v4;
                }

                if ((DataFlags & 0x200) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk9 = v4;
                }

                if ((DataFlags & 0x400) != 0)
                {
                    var v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _unkHash1 = v4;
                }

                if ((DataFlags & 0x800) != 0)
                {
                    var v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _unkHash2 = v4;
                }

                if ((DataFlags & 0x1000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk10 = v4;
                }

                if ((DataFlags & 0x2000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk11 = v4;
                }

                if ((DataFlags & 0x4000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk12 = v4;
                }

                if ((DataFlags & 0x8000) != 0)
                {
                    var v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _categoryHash = v4;
                }

                if ((DataFlags & 0x10000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk14 = v4;
                }

                if ((DataFlags & 0x20000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk15 = v4;
                }

                if ((DataFlags & 0x40000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk16 = v4;
                }

                if ((DataFlags & 0x80000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk17 = v4;
                }

                if ((DataFlags & 0x100000) != 0)
                {
                    var v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _unkHash3 = v4;
                }

                if ((DataFlags & 0x200000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk18 = v4;
                }

                if ((DataFlags & 0x400000) != 0)
                {
                    var v4 = BitConverter.ToChar(data, position);
                    position++;

                    _unk19 = (byte)v4;
                }

                if ((DataFlags & 0x800000) != 0)
                {
                    var v4 = BitConverter.ToChar(data, position);
                    position++;

                    _unk20 = (byte)v4;
                }

                if ((DataFlags & 0x1000000) != 0)
                {
                    var v4 = BitConverter.ToChar(data, position);
                    position++;

                    _unk21 = (byte)v4;
                }

                if ((DataFlags & 0x2000000) != 0)
                {
                    var v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _unkHash4 = v4;
                }

                if ((DataFlags & 0x4000000) != 0)
                {
                    var v4 = BitConverter.ToUInt32(data, position);
                    position += 4;

                    _unkHash5 = v4;
                }

                if ((DataFlags & 0x8000000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk22 = v4;
                }

                if ((DataFlags & 0x10000000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk23 = v4;
                }

                if ((DataFlags & 0x20000000) != 0)
                {
                    var v4 = BitConverter.ToUInt16(data, position);
                    position += 2;

                    _unk24 = v4;
                }
            }

            return position;
        }
    };
}
