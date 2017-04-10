
namespace RageAudioTool.Interfaces
{
    public interface ISerializable
    {
        byte[] Serialize();
        int Deserialize(byte[] data);
    }
}
