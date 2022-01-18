using System;

namespace Refactor
{
    public sealed class NetworkClientPacketsSender : INetworkClientPacketsSender
    {
        private readonly int _bufferSize;
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        public Guid ClientID { get; private set; }

        public NetworkClientPacketsSender(int bufferSize, UDPClient udpClient, TCPClient tcpClient)
        {
            _bufferSize = bufferSize;
            _udpClient = udpClient;
            _tcpClient = tcpClient;
        }

        public void SetClientID(Guid clientID)
        {
            ClientID = clientID;
        }

        public void SendTCP(WritePacketBase writePacket)
        {
            writePacket.ClientID = ClientID;
            var bytesToSend = GetBytesToSend(writePacket);
            _tcpClient.Send(bytesToSend);
        }

        public void SendUDP(WritePacketBase writePacket)
        {
            writePacket.ClientID = ClientID;
            var bytesToSend = GetBytesToSend(writePacket);
            _udpClient.Send(bytesToSend);
        }

        private byte[] GetBytesToSend(WritePacketBase writePacket)
        {
            writePacket.SetBytes(new byte[_bufferSize]);
            writePacket.WriteBasePacketDataAndSerializePacket();
            var bytesToSend = new byte[writePacket.Lenght];
            Array.Copy(writePacket.GetBytes(), 0, bytesToSend, 0, writePacket.Lenght);
            return bytesToSend;
        }
    }
}