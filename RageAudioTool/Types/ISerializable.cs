namespace RageAudioTool.Types
{
    public interface ISerializable<T>
    {
        int Deserialize(T data);
    }

    public interface ISerializable
    {
        int Deserialize(object data);
    }
}
