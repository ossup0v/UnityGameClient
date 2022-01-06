using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Tmp
{
    public sealed class ClientExample
    {
        private Dictionary<int, IPacketHandleable> packetHandlersByPacketID = new Dictionary<int, IPacketHandleable>();

        [RuntimeInitializeOnLoadMethod]
        public static void Test()
        {
            var qwe = new ClientExample();
            qwe.FindAllPacketHandlers();

            // var qwe2 = qwe.GetPacketHandlerByPacketID<SpawnPlayerPacketHandler>(SpawnPlayerPacket.PacketNumber);
            // Debug.Log(qwe2.GetType());

            var qwe3 = qwe.GetPacketHandlerByPacketID<PlayerMovementPacketHandler>(PlayerMovementPacket.PacketNumber);
            Debug.Log(qwe3.GetType());
        }

        public void FindAllPacketHandlers()
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    var networkPacketAttribute = type.GetCustomAttribute(typeof(NetworkPacketAttribute), false) as NetworkPacketAttribute;
                    if (networkPacketAttribute != null)
                    {
                        var packetNumber = networkPacketAttribute.PacketNumber;
                        var packetHandler = Activator.CreateInstance(type) as IPacketHandleable;
                        packetHandlersByPacketID.Add(packetNumber, packetHandler);
                    }   
                }
            }
        }

        public T GetPacketHandlerByPacketID<T>(int packetNumber) where T : class, IPacketHandleable
        {
            return packetHandlersByPacketID[packetNumber] as T;
        }

        public void Read(byte[] bytes)
        {
            var packetID = 0;
            var readOffset = 0;
            packetHandlersByPacketID[packetID].HandleBytes(bytes, readOffset);
        }

        public void Send(PacketBase packet)
        {
            packet.SerializePacket();
            var bytesToSend = packet.GetBytes();
            // send
        }
    }

    public class PlayerMovementExample : MonoBehaviour, IPacketReceivable<PlayerMovementPacket>
    {
        private ClientExample _clientExample;

        private void Awake()
        {
            var playerMovementPacketHandler = _clientExample.GetPacketHandlerByPacketID<PlayerMovementPacketHandler>(PlayerMovementPacket.PacketNumber);
            playerMovementPacketHandler.SubscribeToPacketHandler(this);
        }

        public void ReceivePacket(PlayerMovementPacket packet)
        {
            // чето делаешь с данными
        }

        private void OnDestroy()
        {
            var playerMovementPacketHandler = _clientExample.GetPacketHandlerByPacketID<PlayerMovementPacketHandler>(PlayerMovementPacket.PacketNumber);
            playerMovementPacketHandler.UnsubscribeFromPacketHandler(this);
        }
    }

    public class SomeListener : IPacketReceivable<SpawnPlayerPacket>
    {
        private ClientExample _clientExample;

        public void Init()
        {
            // var spawnPlayerkPacketHandler = _clientExample.GetPacketHandlerByPacketID<SpawnPlayerPacketHandler>(SpawnPlayerPacket.PacketNumber);
            // spawnPlayerkPacketHandler.SubscribeToPacketHandler(this);
        }

        public void ReceivePacket(SpawnPlayerPacket packet)
        {
            var id = packet.Guid;
            var username = packet.UserName;
            var position = packet.Position;
            var rotation = packet.Rotation;
            var currentWeaponKind = packet.CurrentWeaponKind;
        }
    }

    public interface IPacketReceivable<T> where T : PacketBase
    {
        void ReceivePacket(T packet);
    }

    public interface IPacketHandleable
    {
        void HandleBytes(byte[] packetBytes, int readOffset);
    }

    public class NetworkPacketHandlerBase<T> : IPacketHandleable where T : PacketBase, new()
    {
        protected List<IPacketReceivable<T>> packetReceivables = new List<IPacketReceivable<T>>();

        public void SubscribeToPacketHandler(IPacketReceivable<T> packetReceivable)
        {
            packetReceivables.Add(packetReceivable);
        }

        public void UnsubscribeFromPacketHandler(IPacketReceivable<T> packetReceivable)
        {
            if (packetReceivables.Contains(packetReceivable))
            {
                packetReceivables.Remove(packetReceivable);
            }
        }

        public void HandleBytes(byte[] packetBytes, int readOffset)
        {
            var packet = new T();
            packet.SetReadOffset(readOffset);
            packet.SetBytes(packetBytes);
            packet.DeserializePacket();
            NotifySubscribers(packet);
        }

        protected virtual void NotifySubscribers(T packet)
        {
            foreach (var packetReceivable in packetReceivables)
            {
                packetReceivable.ReceivePacket(packet);
            }
        }
    }

    // [NetworkPacket(SpawnPlayerPacket.PacketNumber)]
    public sealed class SpawnPlayerPacketHandler : NetworkPacketHandlerBase<SpawnPlayerPacket>
    {
    }

    [NetworkPacket(PlayerMovementPacket.PacketNumber)]
    public sealed class PlayerMovementPacketHandler : NetworkPacketHandlerBase<PlayerMovementPacket>
    {
    }

    public abstract class PacketBase
    {
        protected byte[] _packetBytes;
        public virtual int ReadOffset { get; protected set; }
        public abstract void SerializePacket();
        public abstract void DeserializePacket();

        public virtual void SetBytes(byte[] packetBytes)
        {
            _packetBytes = packetBytes;
        }

        public void SetReadOffset(int readOffset)
        {
            ReadOffset = readOffset;
        }

        public virtual byte[] GetBytes()
        {
            return _packetBytes;
        }
    }

    public sealed class PlayerMovementPacket : PacketBase
    {
        public const int PacketNumber = 2;
        public override void DeserializePacket()
        {
            throw new NotImplementedException();
        }

        public override void SerializePacket()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class SpawnPlayerPacket : PacketBase
    {
        public const int PacketNumber = 1;
        public Guid Guid;
        public string UserName;
        public Vector3 Position;
        public Quaternion Rotation;
        public int CurrentWeaponKind;

        public override void DeserializePacket()
        {
            throw new NotImplementedException();
        }

        public override void SerializePacket()
        {
            this.Write(2);
            throw new NotImplementedException();
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class NetworkPacketAttribute : Attribute
    {
        public int PacketNumber { get; private set; }
        public NetworkPacketAttribute(int packetNumber)
        {
            PacketNumber = packetNumber;
        }
    }

    public static class PacketWriteHelper
    {
        public static void Write(this PacketBase packetBase, int value)
        {

        }
    }
}