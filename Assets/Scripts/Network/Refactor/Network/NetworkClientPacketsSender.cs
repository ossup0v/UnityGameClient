using System;

namespace Refactor
{
    public class NetworkClientPacketsSender : INetworkClientPacketsSender
    {
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        public Guid ClientGUID { get; set; }

        public NetworkClientPacketsSender(UDPClient udpClient, TCPClient tcpClient)
        {
            _udpClient = udpClient;
            _tcpClient = tcpClient;
        }

        public void SendTCP(WritePacketBase writePacket)
        {
            writePacket.GUID = ClientGUID;
            var bytesToSend = GetBytesToSend(writePacket);
            _tcpClient.Send(bytesToSend);
        }

        public void SendUDP(WritePacketBase writePacket)
        {
            writePacket.GUID = ClientGUID;
            var bytesToSend = GetBytesToSend(writePacket);
            _udpClient.Send(bytesToSend);
        }

        private byte[] GetBytesToSend(WritePacketBase writePacket)
        {
            writePacket.SerializePacket();
            var bytesToSend = new byte[writePacket.GetBytes().Length];
            Array.Copy(writePacket.GetBytes(), 0, bytesToSend, 0, bytesToSend.Length);
            return bytesToSend;
        }
    }
}
