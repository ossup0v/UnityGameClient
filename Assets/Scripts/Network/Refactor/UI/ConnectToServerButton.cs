using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Refactor
{
    public class ConnectToServerButton : NetworkMonoBehaviour
    {
        [SerializeField] private Button _connectToServerButton;
        private HelloReadPacketReceiver _helloReadPacketReceiver; // TODO: убрать это отсюда

        private void Awake()
        {
            _connectToServerButton.onClick.AddListener(OnConnectToServerButtonClicked);
        }

        private void OnConnectToServerButtonClicked()
        {
            var ip = "127.0.0.1"; // TODO: вынести в другой объект подключение
            var port = 29000;
            _helloReadPacketReceiver = new HelloReadPacketReceiver(_networkClient.NetworkClientPacketsSender, _networkClient.PacketHandlersHolder);
            _networkClient.Connect(ip, port, OnConnectedToUDPServer, OnConnectedToTcpServer);

            void OnConnectedToUDPServer()
            {

            }

            void OnConnectedToTcpServer()
            {
                var helloPacket = new HelloWritePacket();
                _networkClient.NetworkClientPacketsSender.SendTCP(helloPacket);
            }
        }

        private void OnDestroy()
        {
            _helloReadPacketReceiver.Dispose();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (_connectToServerButton == null)
            {
                _connectToServerButton = GetComponent<Button>();
            }
        }
#endif
    }
}
