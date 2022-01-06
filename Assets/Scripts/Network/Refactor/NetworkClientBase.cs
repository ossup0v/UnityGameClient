using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    // [CreateAssetMenu(fileName = "NetworkClient", menuName = "Network/NetworkClient", order = 0)]
    public abstract class NetworkClientBase : ScriptableObject
    {
        [SerializeField] private NetworkClientReceiverBase _networkClientReceiver;
        [SerializeField] private NetworkClientSenderBase _networkClientSender;
    }
}