using System;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientReceiver", menuName = "Network/NetworkClientReceiver", order = 0)]
    public class NetworkClientReceiver : ScriptableObject, IBytesReadable, IPacketHandlersHolder
    {
        private Dictionary<int, IPacketHandleable> packetHandlersByPacketID = new Dictionary<int, IPacketHandleable>();
        
        public void Init()
        {
            PacketHandlersHolderHelper.FindAllPacketHandlersFor<NetworkClientReceiver>(packetHandlersByPacketID);
        }

        public void ReadBytes(byte[] bytes)
        {
            var readOffset = sizeof(int);
            var packetID = BitConverter.ToInt32(bytes, 0);
            GetPacketHandlerByPacketID(packetID).HandleBytes(bytes, readOffset);
        }

        public IPacketHandleable GetPacketHandlerByPacketID(int packetID)
        {
            if (packetHandlersByPacketID.TryGetValue(packetID, out var packetHandler))
            {
                return packetHandler;
            }
            else
            {
                Logger.WriteError(nameof(NetworkClientReceiver), $"Can't find packet handler with ID {packetID}");
                return default;
            }
        }
    }
}