using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    public abstract class NetworkMonoBehaviour : MonoBehaviour
    {
        [SerializeField] private NetworkRoomClientProvider _networkRoomClientProvider;

        protected INetworkClient _networkClient => _networkRoomClientProvider.NetworkRoomClient;

#if UNITY_EDITOR
        protected virtual void OnValidate()
        {
            if (_networkRoomClientProvider == null)
            {
                _networkRoomClientProvider = Resources.Load<NetworkRoomClientProvider>(nameof(NetworkRoomClientProvider));
            }
        }
#endif
    }
}