﻿using System;
using System.Xml.Serialization;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public class audGranularSound : audSoundBase
    {
        [XmlElement(DataType = "hexBinary")]
        public byte[] Data { get; set; }

        public override byte[] Serialize()
        {
            var bytes = base.Serialize();

            Buffer.BlockCopy(bytes, 0, Data, 0, bytes.Length);

            return Data;
        }

        public override int Deserialize(byte[] data)
        {
            int bytesRead = base.Deserialize(data);

            Data = data;

            return data.Length;
        }

        public override string ToString()
        {
            return BitConverter.ToString(Data).Replace("-", "");
        }

        public audGranularSound(RageDataFile parent, string str) : base(parent, str)
        { }

        public audGranularSound(RageDataFile parent, uint hashName) : base(parent, hashName)
        { }

        public audGranularSound()
        { }
    }
}
