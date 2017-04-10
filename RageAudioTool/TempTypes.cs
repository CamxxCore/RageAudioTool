using System;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.IO;
using RageAudioTool.Types;
using RageAudioTool.Interfaces;

namespace RageAudioTool
{
    /*
dat151 parameter object hash e.x "SPEECH_PARAMS_FORCE_SHOUTED"
 * */
    /*   [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
       struct dat151
       {
           [FieldOffset(0x4)]
           uint Flags;
           [FieldOffset(0x10)]
           byte unkByte;
           [FieldOffset(0x11)]
           byte unkByte1;
           [FieldOffset(0x18)]
           int unkInt;
       }

       struct float

       [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
       struct audShorelinePool
       {
           [FieldOffset(0x40)]
           short Count;
           [FieldOffset(0x44)]
           //[MarshalAs(UnmanagedType.LPArray, SizeConst = )]

       }*/

    public enum dat54_audMetadataTypes // ref 0x141199C1C
    {
        LoopingSound = 1,
        EnvelopeSound = 2,
        TwinLoopSound = 3,
        SpeechSound = 4,
        OnStopSound = 5,
        WrapperSound = 6,
        SequentialSound = 7,
        StreamingSound = 8,
        RetriggeredOverlappedSound = 9,
        CrossfadeSound = 10,
        CollapsingStereoSound = 11,
        SimpleSound = 12,
        MultitrackSound = 13,
        RandomizedSound = 14,
        EnvironmentSound = 15,
        DynamicEntitySound = 16,
        SequentialOverlapSound = 17,
        ModularSynthSound = 18,
        GranularSound = 19,
        DirectionalSound = 20,
        KineticSound = 21,
        SwitchSound = 22,
        VariableCurveSound = 23,
        VariablePrintValueSound = 24,
        VariableBlockSound = 25,
        IfSound = 26,
        MathOperationSound = 27,
        ParameterTransformSound = 28,
        FluctuatorSound = 29,
        AutomationSound = 30,
        ExternalStreamSound = 31,
        SoundSet = 32
    }

    public enum dat151_audMetadataTypes // ref 0x141199C1C
    {
        SpeechParams = 14,
        StartTrackAction = 63,
        StopTrackAction = 64,
        SetMoodAction = 65,
        StartOneShotAction = 67,
        StopOneShotAction = 68,
        AnimalParams = 73,
        ShoreLinePool = 90,
        ShoreLineLake = 91,
        ShoreLineRiver = 92,
        ShoreLineOcean = 93,
        ShoreLineList = 94,
        RadioDjSpeechAction = 98,
        FadeOutRadioAction = 102,
        FadeInRadioAction = 103,
        ForceRadioTrackAction = 104
    }

 /*   struct filePtrPair
    {
        public uint A, B;
    }*/

    public enum dat4_speechMetadataTypes // ref 0x141199C1C
    {
        Unk = 1,
    }

    public class audCommonVariable<T>
    {
        public T Variable { get; set; }
        public bool Active { get; }    

        public audCommonVariable(T variable, bool active)
        {
            Variable = variable;
            Active = active;
        }
        
        public static implicit operator T (audCommonVariable<T> item)
        {
            if (item != null)
                return item.Variable;
            else return default(T);
        }

        public static implicit operator audCommonVariable<T>(T item)
        {
            return new audCommonVariable<T>(item, true);
        }
    }

    public struct audSoundHeader : ISerializable
    {
        public byte SoundType;

        public uint DataFlags;

        public audCommonVariable<uint> UnkHash
        {
            get { return unkHash; }

            set
            {
                unkHash = value;
             //   if (value.Active)
              //  DataFlags |= 1;
              //  else DataFlags ^= 1;
            }
        }
        private audCommonVariable<uint> unkHash;

        public audCommonVariable<ushort> Unk1
        {
            get { return unk1; }

            set
            {
                unk1 = value;
              //  DataFlags |= 2;
            }
        }
        private audCommonVariable<ushort> unk1;

        public audCommonVariable<ushort> Unk2
        {
            get { return unk2; }
            set
            {
                unk2 = value;
             //   DataFlags |= 4;
            }
        }
        private audCommonVariable<ushort> unk2;

        public audCommonVariable<ushort> Unk3
        {
            get { return unk3; }
            set
            {
                unk3 = value;
              //  DataFlags |= 8;
            }
        }
        private audCommonVariable<ushort> unk3; //0xD-0xF

        public audCommonVariable<ushort> Unk4
        {
            get { return unk4; }
            set
            {
                unk4 = value;
              //  DataFlags |= 0x10;
            }
        }
        private audCommonVariable<ushort> unk4; //0xF-0x11

        public audCommonVariable<ushort> Unk5
        {
            get { return unk5; }
            set
            {
                unk5 = value;
              //  DataFlags |= 0x20;
            }
        }
        private audCommonVariable<ushort> unk5; //0x11-0x13

        public audCommonVariable<ushort> Unk6
        {
            get { return unk6; }
            set
            {
                unk6 = value;
              //  DataFlags |= 0x40;
            }
        }
        private audCommonVariable<ushort> unk6; //0x13-0x15

        public audCommonVariable<ushort> Unk7
        {
            get { return unk7; }
            set
            {
                unk7 = value;
              //  DataFlags |= 0x80;
            }
        }
        private audCommonVariable<ushort> unk7; //0x15-0x17

        public audCommonVariable<ushort> Unk8
        {
            get { return unk8; }
            set
            {
                unk8 = value;
              //  DataFlags |= 0x100;
            }
        }
        private audCommonVariable<ushort> unk8; //0x17-0x19

        public audCommonVariable<ushort> Unk9
        {
            get { return unk9; }
            set
            {
                unk9 = value;
              //  DataFlags |= 0x200;
            }
        }
        private audCommonVariable<ushort> unk9; //0x19-0x1B

        public uint UnkHash1
        {
            get { return unkHash1; }
            set
            {
                unkHash1 = value;
               // DataFlags |= 0x400;
            }
        }
        private uint unkHash1; //0x1B-0x1F

        public uint UnkHash2
        {
            get { return unkHash2; }
            set
            {
                unkHash2 = value;
               // DataFlags |= 0x800;
            }
        }
        private uint unkHash2; //0x1F-0x23

        public audCommonVariable<ushort> Unk10
        {
            get { return unk10; }
            set
            {
                unk10 = value;
                DataFlags |= 0x1000;
            }
        }
        private audCommonVariable<ushort> unk10; //0x23-0x25

        public audCommonVariable<ushort> Unk11
        {
            get { return unk11; }
            set
            {
                unk11 = value;
              //  DataFlags |= 0x2000;
            }
        }
        private audCommonVariable<ushort> unk11; //0x25-0x27

        public audCommonVariable<ushort> Unk12
        {
            get { return unk12; }
            set
            {
                unk12 = value;
              //  DataFlags |= 0x4000;
            }
        }
        private audCommonVariable<ushort> unk12; //0x27-0x29

        public uint CategoryHash
        {
            get { return categoryHash; }
            set
            {
                categoryHash = value;
              //  DataFlags |= 0x8000;
            }
        }
        private uint categoryHash; //0x29-0x2D //

        public audCommonVariable<ushort> Unk14
        {
            get { return unk14; }
            set
            {
                unk14 = value;
              //  DataFlags |= 0x10000;
            }
        }
        private audCommonVariable<ushort> unk14; //0x2D-0x2F

        public audCommonVariable<ushort> Unk15
        {
            get { return unk15; }
            set
            {
                unk15 = value;
             //   DataFlags |= 0x20000;
            }
        }
        private audCommonVariable<ushort> unk15; //0x2F-0x31

        public audCommonVariable<ushort> Unk16
        {
            get { return unk16; }
            set
            {
                unk16 = value;
             //   DataFlags |= 0x40000;
            }
        }
        private audCommonVariable<ushort> unk16; //0x31-0x33

        public audCommonVariable<ushort> Unk17
        {
            get { return unk17; }
            set
            {
                unk17 = value;
              //  DataFlags |= 0x80000;
            }
        }
        private audCommonVariable<ushort> unk17; //0x33-0x35

        public uint UnkHash3
        {
            get { return unkHash3; }
            set
            {
                unkHash3 = value;
              //  DataFlags |= 0x100000;
            }
        }
        private uint unkHash3; //0x35-0x39

        public audCommonVariable<ushort> Unk18
        {
            get { return unk18; }
            set
            {
                unk18 = value;
             //   DataFlags |= 0x200000;
            }
        }
        private audCommonVariable<ushort> unk18; //0x39-0x3B

        public audCommonVariable<byte> Unk19
        {
            get { return unk19; }
            set
            {
                unk19 = value;
              //  DataFlags |= 0x400000;
            }
        }
        private audCommonVariable<byte> unk19; //0x3B-0x3C

        public audCommonVariable<byte> Unk20
        {
            get { return unk20; }
            set
            {
                unk20 = value;
              //  DataFlags |= 0x800000;
            }
        }
        private audCommonVariable<byte> unk20; //0x3C-0x3D

        public audCommonVariable<byte> Unk21
        {
            get { return unk21; }
            set
            {
                unk21 = value;
              //  DataFlags |= 0x1000000;
            }
        }
        private audCommonVariable<byte> unk21; //0x3D-0x3E

        public uint UnkHash4
        {
            get { return unkHash4; }
            set
            {
                unkHash4 = value;
             //   DataFlags |= 0x2000000;
            }
        }
        private uint unkHash4; //0x3E-0x42

        public uint UnkHash5
        {
            get { return unkHash5; }
            set
            {
                unkHash5 = value;
             //   DataFlags |= 0x4000000;
            }
        }
        private uint unkHash5; //0x42-0x46

        public audCommonVariable<ushort> Unk22
        {
            get { return unk22; }
            set
            {
                unk22 = value;
             //   DataFlags |= 0x8000000;
            }
        }
        private audCommonVariable<ushort> unk22; //0x46-0x48

        public audCommonVariable<ushort> Unk23
        {
            get { return unk23; }
            set
            {
                unk23 = value;
              //  DataFlags |= 0x10000000;
            }
        }
        private audCommonVariable<ushort> unk23; //0x48-0x4A

        public audCommonVariable<ushort> Unk24
        {
            get { return unk24; }
            set
            {
                unk24 = value;
              //  DataFlags |= 0x12000000;
            }
        }
        private audCommonVariable<ushort> unk24; //0x4A-0x4C

        public byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(SoundType);

                    writer.Write(DataFlags);

                    if ((DataFlags & 1) != 0)
                    {
                        writer.Write(unkHash);
                    }

                    if ((DataFlags & 2) != 0)
                    {
                        writer.Write(unk1);
                    }

                    if ((DataFlags & 4) != 0)
                    {
                        writer.Write(unk2);
                    }

                    if ((DataFlags & 8) != 0)
                    {
                        writer.Write(unk3);
                    }

                    if ((DataFlags & 0x10) != 0)
                    {
                        writer.Write(unk4);
                    }

                    if ((DataFlags & 0x20) != 0)
                    {
                        writer.Write(unk5);
                    }

                    if ((DataFlags & 0x40) != 0)
                    {
                        writer.Write(unk6);
                    }

                    if ((DataFlags & 0x80) != 0)
                    {
                        writer.Write(unk7);
                    }

                    if ((DataFlags & 0x100) != 0)
                    {
                        writer.Write(unk8);
                    }

                    if ((DataFlags & 0x200) != 0)
                    {
                        writer.Write(unk9);
                    }
                    if ((DataFlags & 0x400) != 0)
                    {
                        writer.Write(unkHash1);
                    }

                    if ((DataFlags & 0x800) != 0)
                    {
                        writer.Write(unkHash2);
                    }

                    if ((DataFlags & 0x1000) != 0)
                    {
                        writer.Write(unk10);
                    }

                    if ((DataFlags & 0x2000) != 0)
                    {
                        writer.Write(unk11);
                    }

                    if ((DataFlags & 0x4000) != 0)
                    {
                        writer.Write(unk12);
                    }

                    if ((DataFlags & 0x8000) != 0)
                    {
                        writer.Write(CategoryHash);
                    }

                    if ((DataFlags & 0x10000) != 0)
                    {
                        writer.Write(unk14);
                    }

                    if ((DataFlags & 0x20000) != 0)
                    {
                        writer.Write(unk15);
                    }

                    if ((DataFlags & 0x40000) != 0)
                    {
                        writer.Write(unk16);
                    }

                    if ((DataFlags & 0x80000) != 0)
                    {
                        writer.Write(unk17);
                    }

                    if ((DataFlags & 0x100000) != 0)
                    {
                        writer.Write(unkHash3);
                    }

                    if ((DataFlags & 0x200000) != 0)
                    {
                        writer.Write(unk18);
                    }

                    if ((DataFlags & 0x400000) != 0)
                    {
                        writer.Write(unk19);
                    }

                    if ((DataFlags & 0x800000) != 0)
                    {
                        writer.Write(unk20);
                    }

                    if ((DataFlags & 0x1000000) != 0)
                    {
                        writer.Write(unk21);
                    }

                    if ((DataFlags & 0x2000000) != 0)
                    {
                        writer.Write(unkHash4);
                    }

                    if ((DataFlags & 0x4000000) != 0)
                    {
                        writer.Write(unkHash5);
                    }

                    if ((DataFlags & 0x8000000) != 0)
                    {
                        writer.Write(unk22);
                    }

                    if ((DataFlags & 0x10000000) != 0)
                    {
                        writer.Write(unk23);
                    }

                    if ((DataFlags & 0x20000000) != 0)
                    {
                        writer.Write(unk24);
                    }
                }

                return stream.ToArray();
            }
        }

        public int Deserialize(byte[] data)
        {
            int position = 0;

            SoundType = data[position];

            position++;

            DataFlags = BitConverter.ToUInt32(data, position);

            position += 4;

            if ((DataFlags & 1) != 0)
            {
                uint v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                unkHash = v4;
            }

            if ((DataFlags & 2) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk1 = v4;
            }

            if ((DataFlags & 4) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk2 = v4;
            }

            if ((DataFlags & 8) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk3 = v4;
            }

            if ((DataFlags & 0x10) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk4 = v4;
            }

            if ((DataFlags & 0x20) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk5 = v4;
            }

            if ((DataFlags & 0x40) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk6 = v4;
            }

            if ((DataFlags & 0x80) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk7 = v4;
            }

            if ((DataFlags & 0x100) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk8 = v4;
            }

            if ((DataFlags & 0x200) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk9 = v4;
            }

            if ((DataFlags & 0x400) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                unkHash1 = v4;
            }

            if ((DataFlags & 0x800) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                unkHash2 = v4;
            }

            if ((DataFlags & 0x1000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk10 = v4;
            }

            if ((DataFlags & 0x2000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk11 = v4;
            }

            if ((DataFlags & 0x4000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk12 = v4;
            }

            if ((DataFlags & 0x8000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                CategoryHash = v4;
            }

            if ((DataFlags & 0x10000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk14 = v4;
            }

            if ((DataFlags & 0x20000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk15 = v4;
            }

            if ((DataFlags & 0x40000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk16 = v4;
            }

            if ((DataFlags & 0x80000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk17 = v4;
            }

            if ((DataFlags & 0x100000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                unkHash3 = v4;
            }

            if ((DataFlags & 0x200000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk18 = v4;
            }

            if ((DataFlags & 0x400000) != 0)
            {
                var v4 = BitConverter.ToChar(data, position);
                position++;

                unk19 = (byte)v4;
            }

            if ((DataFlags & 0x800000) != 0)
            {
                var v4 = BitConverter.ToChar(data, position);
                position++;

                unk20 = (byte)v4;
            }

            if ((DataFlags & 0x1000000) != 0)
            {
                var v4 = BitConverter.ToChar(data, position);
                position++;

                unk21 = (byte)v4;
            }

            if ((DataFlags & 0x2000000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                unkHash4 = v4;
            }

            if ((DataFlags & 0x4000000) != 0)
            {
                var v4 = BitConverter.ToUInt32(data, position);
                position += 4;

                unkHash5 = v4;
            }

            if ((DataFlags & 0x8000000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk22 = v4;
            }

            if ((DataFlags & 0x10000000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk23 = v4;
            }

            if ((DataFlags & 0x20000000) != 0)
            {
                var v4 = BitConverter.ToUInt16(data, position);
                position += 2;

                unk24 = v4;
            }

            return position;
        }
    };

    /*   [StructLayout(LayoutKind.Sequential)]
       public struct audCommonSoundData
       {
           public byte soundType; //0x0-0x1
           public uint dataFlags; //0x1-0x5
           public uint unkHash; //0x5-0x9
           public ushort unk1; //0x9-0xB
           public ushort unk2; //0xB-0xD
           public ushort unk3; //0xD-0xF
           public ushort unk4; //0xF-0x11
           public ushort unk5; //0x11-0x13
           public ushort unk6; //0x13-0x15
           public ushort unk7; //0x15-0x17
           public ushort unk8; //0x17-0x19
           public ushort unk9; //0x19-0x1B //
           public uint unkHash1; //0x1B-0x1F HASH ITEM1
           public uint unkHash2; //0x1F-0x23
           public ushort unk10; //0x23-0x25
           public ushort unk11; //0x25-0x27
           public ushort unk12; //0x27-0x29
           public uint categoryHash; //0x29-0x2D //
           public ushort unk14; //0x2D-0x2F
           public ushort unk15; //0x2F-0x31
           public ushort unk16; //0x31-0x33
           public ushort unk17; //0x33-0x35
           public uint unkHash3; //0x35-0x39
           public ushort unk18; //0x39-0x3B
           public byte unk19; //0x3B-0x3C
           public byte unk20; //0x3C-0x3D
           public byte unk21; //0x3D-0x3E
           public uint unkHash4; //0x3E-0x42
           public uint unkHash5; //0x42-0x46
           public ushort unk22; //0x46-0x48
           public ushort unk23; //0x48-0x4A
           public ushort unk24; //0x4A-0x4C
       };*/
}
