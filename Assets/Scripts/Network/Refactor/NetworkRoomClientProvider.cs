using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkRoomClientProvider", menuName = "Network/NetworkRoomClientProvider", order = 0)]
    public class NetworkRoomClientProvider : NetworkClientProvider
    {
        private NetworkRoomClient _networkRoomClient;
        public override INetworkClient NetworkClient => _networkRoomClient;

        public void CreateNetworkRoomClient()
        {
            _networkRoomClient = new NetworkRoomClient();
        }
    }
}