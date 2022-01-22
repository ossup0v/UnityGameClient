using System;
using UnityEngine;
using UnityEngine.UI;

namespace Refactor
{
    public class ConnectToServerButton : NetworkMonoBehaviour
    {
        [SerializeField] private Button _connectToServerButton;
        [SerializeField] private ClientRoomEnterRoom _clientRoomEnterRoom;
        [SerializeField] private GameObject _connectToRoomWindow; // TODO: переделать

        private void Awake()
        {
            _connectToServerButton.onClick.AddListener(OnConnectToServerButtonClicked);
        }

        private void OnConnectToServerButtonClicked()
        {
            if (_clientRoomEnterRoom.TryConnectAndEnterRoom())
            {
                _clientRoomEnterRoom.EnteredRoom += OnEnteredRoom;
            }
        }

        private void OnEnteredRoom()
        {
            _connectToRoomWindow.gameObject.SetActive(false); // TODO: переделать на систему окон
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
