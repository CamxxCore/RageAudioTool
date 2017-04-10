using System;
using System.IO;
using System.Xml.Serialization;
using System.ComponentModel;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public abstract class audFiletypeValue<T> : audFiletypeValue
    {
        public audFiletypeValue(RageDataFile parent, string name, T val) : base(parent, name)
        {
            Value = val;
        }

        public audFiletypeValue(RageDataFile parent, uint hashKey, T val) : base(parent, hashKey)
        {
            Value = val;
        }

        public audFiletypeValue(RageDataFile parent, uint hashKey) : base(parent, hashKey)
        { }

        public audFiletypeValue(RageDataFile parent, string name) : base(parent, name)
        { }

        public audFiletypeValue(RageDataFile parent, T data) : this(parent, "", data)
        { }

        public audFiletypeValue(RageDataFile parent) : this(parent, "")
        { }

        public override string ToString()
        {
            return Value.ToString();
        }

        public virtual T Value { get; set; }
    }

    public abstract class audFiletypeValue : audDataBase
    {
        [XmlIgnore]
        [Browsable(false)]
        public int Type { get; set; }

        [XmlIgnore]
        [Browsable(false)]
        public int Flags { get; set; }

        public override int Deserialize(byte[] data)
        {
            using (BinaryReader reader = new BinaryReader(new MemoryStream(data)))
            {
                Type = reader.ReadInt32();

                Flags = reader.ReadInt32();

                return 8;
            }
        }

        public override byte[] Serialize()
        {
            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    writer.Write(Type);

                    writer.Write(Flags);
                }

                return stream.ToArray();
            }
        }

        public audFiletypeValue(RageDataFile parent, string name) : 
            base(parent, name)
        { }

        public audFiletypeValue(RageDataFile parent, uint hashKey) :
         base(parent, hashKey)
        { }
    }
}
