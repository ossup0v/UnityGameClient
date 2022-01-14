public interface IPacketReceivable<T> where T : PacketBase
{
    void ReceivePacket(T packet);
}