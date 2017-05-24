using System.ComponentModel;
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

                else _hashName = string.Format("0x{0}", _hashKey.ToString("X"));
            }
        }

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
                _hashKey = Utility.HashKey(_hashName);
            }
        }    

        public audHashString(RageDataFile file, uint hash)
        {
            _parent = file;
            _hashKey = hash;

            string str;
            if (_parent.Nametable.TryGetValue(_hashKey, out str))
            {
                _hashName = str;
            }

            else _hashName = string.Format("0x{0}", _hashKey.ToString("X"));
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
            _hashKey = Utility.HashKey(_hashName);
        }

        public static implicit operator string(audHashString hs)
        {
            return hs.HashName;
        }

        public static implicit operator uint(audHashString hs)
        {
            return hs.HashKey;
        }

        public override string ToString()
        {
            return HashName;
        }
    }
}
