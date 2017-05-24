using System;
using System.ComponentModel;
using System.Collections.Generic;
using RageAudioTool.Rage_Wrappers.DatFile.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    /// <summary>
    /// Represents a data entry in an audio file.
    /// </summary>
    /// 
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class audSoundBase : audDataBase
    {
        public int DataOffset { get; private set; }

        [ReadOnly(false)]
        public audSoundHeader Header { get; }

        [ReadOnly(false)]
        public List<audHashString> AudioTracks { get; } = new List<audHashString>();

        /// <summary>
        /// Initialize the class with the hashed name of the data.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="hashName">Data object hash.</param>
        public audSoundBase(RageDataFile parent, uint hashName) : base(parent, hashName)
        {
            Header = new audSoundHeader(parent);
        }

        /// <summary>
        /// Initialize the class with the string name of the data.
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="str">Data object name.</param>
        public audSoundBase(RageDataFile parent, string str) : base(parent, str)
        {
            Header = new audSoundHeader(parent);
        }

        /// <summary>
        /// Default parameterless constructor for serialization.
        /// </summary>
        public audSoundBase()
        { }

        public override byte[] Serialize()
        {
            return Header.Serialize();
        }

        public override int Deserialize(byte[] data)
        {
            AudioTracks.Clear();

            DataOffset = Header.Deserialize(data);

            return DataOffset;
        }
    }
}
