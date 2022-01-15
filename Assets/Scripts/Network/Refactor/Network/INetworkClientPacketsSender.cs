namespace Refactor
{
    public interface INetworkClientPacketsSender
    {
        System.Guid ClientGUID { get; set; }
        void SendTCP(WritePacketBase writePacket);
        void SendUDP(WritePacketBase writePacket);
    }
}
