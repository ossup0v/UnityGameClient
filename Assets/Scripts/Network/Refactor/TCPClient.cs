using System;
using System.Net.Sockets;

namespace Refactor
{
    public class TCPClient
    {
        private TcpClient _tcpClient;
        private NetworkStream _networkStream;
        private byte[] _receiveBuffer;
        private IBytesReadable _bytesReadable;
        private readonly int _bufferSize;

        public TCPClient(int bufferSize, IBytesReadable bytesReadable)
        {
            _bufferSize = bufferSize;
            _bytesReadable = bytesReadable;
        }

        public void Connect(string ip, int port)
        {
            _tcpClient = new TcpClient();
            _tcpClient.ReceiveBufferSize = _bufferSize;
            _tcpClient.SendBufferSize = _bufferSize;
            Logger.WriteLog(nameof(Connect), $"Trying connect to server {ip}:{port}");
            _tcpClient.BeginConnect(ip, port, OnConnected, null);
        }

        public void CloseConnection()
        {
            _networkStream.Close();
            _tcpClient.Close();
            _tcpClient = null;
        }

        public void Send(byte[] bytes)
        {
            try
            {
                if (_tcpClient != null)
                {
                    _networkStream.BeginWrite(bytes, 0, bytes.Length, null, null);
                }
            }
            catch (Exception exception)
            {
                Logger.WriteError(nameof(Send), exception.Message);
            }
        }

        private void OnConnected(IAsyncResult asyncResult)
        {
            _tcpClient.EndConnect(asyncResult);
            
            if (_tcpClient.Connected == false)
            {
                Logger.WriteError(nameof(OnConnected), "Connection not established");
                CloseConnection();
                return;
            }

            _networkStream = _tcpClient.GetStream();
            BeginRead();
        }

        private void BeginRead()
        {
            _networkStream.BeginRead(_receiveBuffer, 0, _bufferSize, OnReaded, null);
        }

        private void OnReaded(IAsyncResult asyncResult)
        {
            try
            {
                var readedNumberOfBytes = _networkStream.EndRead(asyncResult);
                if (readedNumberOfBytes <= 0)
                {
                    Logger.WriteError(nameof(OnReaded), $"Readed {readedNumberOfBytes} bytes.");
                    CloseConnection();
                    return;
                }
                // TODO: нужно будет убрать создание нового массива
                var readedBytes = new byte[readedNumberOfBytes];
                Array.Copy(_receiveBuffer, readedBytes, readedNumberOfBytes);
                
                _bytesReadable.ReadBytes(readedBytes);

                BeginRead();
            }
            catch (Exception exception)
            {
                Logger.WriteError(nameof(OnReaded), exception.Message);
                CloseConnection();
            }
        }
    }
}