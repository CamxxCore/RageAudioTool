using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class audHashString
    {
        private readonly RageDataFile _parent;

        private uint _hashKey;

        private string _hashName;

        [ReadOnly(false)]
        [TypeConverter(typeof(UInt32HexTypeConverter))]
        [XmlIgnore]
        public uint HashKey
        {
            get
            {
                return _hashKey;
            }

            set
            {
                _hashKey = value;

                string str;
                if (_parent != null && _parent.Nametable.TryGetValue(_hashKey, out str))
                {
                    _hashName = str;
                }

                else _hashName = $"0x{_hashKey:X}";
            }
        }

        [XmlText]
        [ReadOnly(false)]      
        public string HashName
        {
            get
            {
                return _hashName;
            }
            set
            {
                _hashName = value;

                if (_hashName.StartsWith("0x"))
                {
                    _hashKey = uint.Parse(_hashName.Substring(2),
                        NumberStyles.HexNumber, CultureInfo.CurrentCulture);
                }
                else if (!uint.TryParse(_hashName, out _hashKey))
                {
                    _hashKey = _hashName.HashKey();
                }
            }
        }    

        public audHashString(RageDataFile file, uint hash)
        {
            _parent = file;
            _hashKey = hash;

            string str;
            if (file != null && file.Nametable.TryGetValue(_hashKey, out str))
            {
                _hashName = str;
            }

            else _hashName = $"0x{_hashKey:X}";
        }

        public audHashString(uint hash)
        {
            _hashKey = hash;
        }

        public audHashString(string str)
        {
            _hashName = str;
        }

        public audHashString()
        { }

        public audHashString(RageDataFile file, string str)
        {
            _parent = file;
            _hashName = str;
            _hashKey = _hashName.HashKey();
        }

        public static implicit operator string(audHashString hs)
        {
            return hs?.HashName;
        }

        public static implicit operator uint(audHashString hs)
        {
            return hs?.HashKey ?? 0;
        }

        public override string ToString()
        {
            return HashName;
        }
    }
}
