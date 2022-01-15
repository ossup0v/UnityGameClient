using System;

namespace Refactor
{
    public class NetworkClientPacketsSender : INetworkClientPacketsSender
    {
        private readonly int _bufferSize;
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        public Guid ClientGUID { get; set; }

        public NetworkClientPacketsSender(int bufferSize, UDPClient udpClient, TCPClient tcpClient)
        {
            _bufferSize = bufferSize;
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
            writePacket.SetBytes(new byte[_bufferSize]);
            writePacket.SerializePacket();
            var bytesToSend = new byte[writePacket.GetBytes().Length];
            Array.Copy(writePacket.GetBytes(), 0, bytesToSend, 0, bytesToSend.Length);
            return bytesToSend;
        }
    }
}
