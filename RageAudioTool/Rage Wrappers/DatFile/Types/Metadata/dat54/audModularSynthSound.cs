﻿using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audModularSynthSound : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override int Deserialize(byte[] data)
        {
            var bytesRead = base.Deserialize(data);

            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return BitConverter.ToString(Data).Replace("-", "");
        }

        public audModularSynthSound(string str) : base(str)
        { }

        public audModularSynthSound(uint hashName) : base(hashName)
        { }

        public audModularSynthSound()
        { }
    }
}
