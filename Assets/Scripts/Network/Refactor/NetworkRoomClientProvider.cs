using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientProvider", menuName = "Network/NetworkClientProvider", order = 0)]
    public class NetworkRoomClientProvider : ScriptableObject
    {
        private NetworkRoomClient _networkRoomClient;
        public INetworkClient NetworkRoomClient => _networkRoomClient;

        public void CreateNetworkRoomClient()
        {
            _networkRoomClient = new NetworkRoomClient();
        }
    }
}