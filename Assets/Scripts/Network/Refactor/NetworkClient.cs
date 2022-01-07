using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tmp;
using UnityEngine;

namespace Refactor
{
    [CreateAssetMenu(fileName = "NetworkClient", menuName = "Network/NetworkClient", order = 0)]
    public class NetworkClient : NetworkClientBase, IBytesReadable, INetworkPacketsHandler
    {
        private const int BufferSize = 1024;
        private UDPClient _udpClient;
        private TCPClient _tcpClient;

        private Dictionary<int, IPacketHandleable> packetHandlersByPacketID = new Dictionary<int, IPacketHandleable>();

        public void Init()
        {
            _udpClient = new UDPClient(BufferSize, this);
            _tcpClient = new TCPClient(BufferSize, this);
            FindAllPacketHandlers<NetworkClient>(packetHandlersByPacketID);
            foreach (var item in packetHandlersByPacketID)
            {
                Debug.Log(item.Key + " " + item.Value.GetType());
            }
        }

        public static void FindAllPacketHandlers<T>(Dictionary<int, IPacketHandleable> packetHandlersByPacketID) where T : class, INetworkPacketsHandler
        {
            Debug.Log("wqeqwe");
            var networkClientType = typeof(T);
            Debug.Log(networkClientType.ToString());
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    var networkPacketAttribute = type.GetCustomAttribute(typeof(NetworkPacketAttribute), false) as NetworkPacketAttribute;
                    if (networkPacketAttribute != null)
                    {
                        var isHasInterface = networkPacketAttribute.PacketHandler.GetInterfaces().Contains(typeof(INetworkPacketsHandler));
                        if (isHasInterface)
                        {
                            if (networkPacketAttribute.PacketHandler == typeof(T))
                            {
                                Debug.Log("qweqwe");
                                var packetNumber = networkPacketAttribute.PacketNumber;
                                var packetHandler = Activator.CreateInstance(type) as IPacketHandleable;
                                packetHandlersByPacketID.Add(packetNumber, packetHandler);
                            }
                        }
                        else
                        {
                            Debug.LogError("У типа нет интерфейса короче"); // TODO: сделать нормальный лог
                        }
                    }   
                }
            }
        }

        public void ReadBytes(byte[] bytes)
        {
            var packetNumber = 0;
        }
    }
}