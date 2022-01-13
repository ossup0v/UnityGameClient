using System;
using System.Collections.Generic;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClientReceiver", menuName = "Network/NetworkClientReceiver", order = 0)]
    public class NetworkClientReceiver : ScriptableObject, IBytesReadable, IPacketHandlersHolder
    {
        [NonSerialized]
        private Dictionary<int, IPacketHandleable> _packetHandlersByPacketID = new Dictionary<int, IPacketHandleable>();
        
        public void Init()
        {
            PacketHandlersHolderHelper.FindAllPacketHandlersFor<NetworkClientReceiver>(_packetHandlersByPacketID);
        }

        public void ReadBytes(ref SocketData socketServerData, byte[] bytes)
        {
            var currentOffset = 0;
            var packetID = BitConverter.ToInt32(bytes, currentOffset);
            currentOffset += 4;
            Logger.WriteLog(nameof(ReadBytes), $"Received packet with ID <b>{packetID}</b>");
            if (IsPacketHandlersContainsPacketID(packetID))
            {
                GetPacketHandlerByPacketID(packetID).HandleBytes(bytes, currentOffset);
            }
            else
            {
                PrintCantFindPacket(packetID);
            }
        }

        public IPacketHandleable GetPacketHandlerByPacketID(int packetID)
        {
            if (_packetHandlersByPacketID.TryGetValue(packetID, out var packetHandler))
            {
                return packetHandler;
            }
            else
            {
                PrintCantFindPacket(packetID);
                return default;
            }
        }

        private void PrintCantFindPacket(int packetID)
        {
            Logger.WriteError(nameof(NetworkClientReceiver), $"Can't find packet handler with ID {packetID}");
        }

        private bool IsPacketHandlersContainsPacketID(int packetID)
        {
            return _packetHandlersByPacketID.ContainsKey(packetID);
        }
    }
}