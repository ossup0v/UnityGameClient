using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientProvider", menuName = "Network/NetworkClientProvider", order = 0)]
    public class NetworkClientProvider : ScriptableObject
    {
        [SerializeField] private NetworkClientBase _networkClientBase;
    }
}