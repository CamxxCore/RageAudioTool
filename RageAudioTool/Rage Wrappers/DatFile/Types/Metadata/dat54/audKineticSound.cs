﻿using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audKineticSound : audSoundBase
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

        public audKineticSound(string str) : base(str)
        { }

        public audKineticSound(uint hashName) : base(hashName)
        { }

        public audKineticSound()
        { }
    }
}
