
namespace RageAudioTool.Rage_Wrappers.DatFile
{
    public interface IRageAudioStringPair
    {
        string Name { get; set; }
        object Data { get; }
    }

    public class DatTypeStringPair<T> : IRageAudioStringPair
    {
        public string Name { get; set; }
        public object Data { get; }

        public DatTypeStringPair(string name, T data)
        {
            Name = name;
            Data = data;
        }

        public DatTypeStringPair(string name, byte[] data)
        {
            Name = name;
            Data = (T) RageAudioDatItem<T>.FromBytes(data);
        }
    }
}
