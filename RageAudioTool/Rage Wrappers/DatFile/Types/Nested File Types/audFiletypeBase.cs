using RageAudioTool.Types;

namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public interface IAudFiletype : ISerializable<byte[]>
    {
        string Name { get; }
        object Data { get; }
    }

    public abstract class audFiletypeBase<T> : IAudFiletype
    {
        public string Name { get; set; }

        public virtual object Data { get; set; }

        public audFiletypeBase(string name, T data)
        {
            Name = name;
            Data = data;
        }

        public audFiletypeBase(string name)
        {
            Name = name;
        }

        public audFiletypeBase(T data) : this("", data)
        { }

        public audFiletypeBase()
        { }

        public override string ToString()
        {
            return Data.ToString();
        }

        public abstract int Deserialize(byte[] data);
    }
}
