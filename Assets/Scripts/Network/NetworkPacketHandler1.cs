using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Some
{
    public interface IPacketHandleable
    {
        void HandlePacket(byte[] packetBytes);
    }

    public abstract class PacketBase
    {
        protected byte[] _packetBytes;
        public abstract void SerializePacket();
        public abstract void DeserializePacket();

        public virtual void SetBytes(byte[] packetBytes)
        {
            _packetBytes = packetBytes;
        }
    }

    public class SpawnPlayerPacket : PacketBase
    {
        public override void DeserializePacket()
        {
            throw new NotImplementedException();
        }

        public override void SerializePacket()
        {
            throw new NotImplementedException();
        }
    }

    public class PacketHandler<T> : IPacketHandleable where T : PacketBase, new()
    {
        public void HandlePacket(byte[] packetBytes)
        {
            throw new NotImplementedException();
        }
    }

    public class SpawnPlayerkPacketHandler : PacketHandler<SpawnPlayerPacket>
    {
    }

    public class ClientExample
    {
        private IPacketHandleable test = new SpawnPlayerkPacketHandler();
        public T GetClass<T>() where T : class, IPacketHandleable
        {
            return test as T;
        }
    }

    public class SomeListener
    {
        private ClientExample _clientExample = new ClientExample();
        [RuntimeInitializeOnLoadMethod]
        public static void Test()
        {
            var some = new SomeListener();
            some.Init();
        }

        public void Init()
        {
            var test = _clientExample.GetClass<SpawnPlayerkPacketHandler>();
            // Debug.Log(test.GetType());
        }
    }
}