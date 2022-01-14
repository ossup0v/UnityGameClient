using System;

namespace Refactor
{
    public class NetworkClientPacketsSender
    {
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        public NetworkClientPacketsSender(UDPClient udpClient, TCPClient tcpClient)
        {
            _udpClient = udpClient;
            _tcpClient = tcpClient;
        }

        public void SendTCP(PacketBase packet)
        {
            var bytesToSend = GetBytesToSend(packet);
            _tcpClient.Send(bytesToSend);
        }

        public void SendUDP(PacketBase packet)
        {
            var bytesToSend = GetBytesToSend(packet);
            _udpClient.Send(bytesToSend);
        }

        private byte[] GetBytesToSend(PacketBase packet)
        {
            packet.SerializePacket();
            var bytesToSend = new byte[packet.GetBytes().Length];
            Array.Copy(packet.GetBytes(), 0, bytesToSend, 0, bytesToSend.Length);
            return bytesToSend;
        }
    }
}
