public interface IPacketHandleable
{
    void HandleBytes(byte[] packetBytes, int readOffset);
}