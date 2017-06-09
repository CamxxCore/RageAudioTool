using System.Runtime.InteropServices;

namespace RageAudioTool
{
    [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Ansi)]
    struct audSound
    {
        [FieldOffset(0x62)]
        private byte parentSoundIdx;
    }

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

        public enum Dat54AudMetadataTypes // ref 0x141199C1C
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
        SoundSet = 32,
        Unknown = 33,
        SoundList = 35
    }

    public enum Dat151AudMetadataTypes // ref 0x141199C1C
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

    public enum Dat4SpeechMetadataTypes // ref 0x141199C1C
    {
        Unk = 1,
    }

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
