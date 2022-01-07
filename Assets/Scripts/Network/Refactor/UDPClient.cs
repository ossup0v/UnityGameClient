using System;
using System.Net;
using System.Net.Sockets;

namespace Refactor
{
    public class UDPClient
    {
        private UdpClient _udpClient;
        private IPEndPoint _endPoint;
        private IBytesReadable _bytesReadable;
        private readonly int _bufferSize;

        public UDPClient(int bufferSize, IBytesReadable bytesReadable)
        {
            _bufferSize = bufferSize;
            _bytesReadable = bytesReadable;
        }

        public void Connect(string ip, int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _udpClient = new UdpClient();
            _udpClient.Connect(_endPoint);
            BeginReceive();
        }

        public void CloseConnection()
        {
            _udpClient.Close();       
            _udpClient = null;     
        }

        public void Send(byte[] bytes)
        {
            if (_udpClient != null)
            {
                _udpClient.BeginSend(bytes, bytes.Length, null ,null);
            }
        }

        private void BeginReceive()
        {
            _udpClient.BeginReceive(OnReceived, null);
        }

        private void OnReceived(IAsyncResult asyncResult)
        {
            try
            {
                var receivedBytes = _udpClient.EndReceive(asyncResult, ref _endPoint);
                BeginReceive();

                if (receivedBytes.Length == 0)
                {
                    Logger.WriteError(nameof(OnReceived), "Received 0 bytes");
                    CloseConnection();
                    return;    
                }

                _bytesReadable.ReadBytes(receivedBytes);
            }
            catch (Exception exception)
            {
                Logger.WriteError(nameof(OnReceived), exception.Message);
            }
        }
    }
}