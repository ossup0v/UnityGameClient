using System.Collections.Generic;
using Refactor;

public abstract class NetworkPacketHandler<T> : IPacketHandleable where T : PacketBase
{
    protected List<IPacketReceivable<T>> packetReceivables = new List<IPacketReceivable<T>>();

    public virtual void SubscribeToPacketHandler(IPacketReceivable<T> packetReceivable)
    {
        packetReceivables.Add(packetReceivable);
    }

    public virtual void UnsubscribeFromPacketHandler(IPacketReceivable<T> packetReceivable)
    {
        if (packetReceivables.Contains(packetReceivable))
        {
            packetReceivables.Remove(packetReceivable);
        }
    }

    public void HandleBytes(ref SocketData socketData, byte[] packetBytes, int readOffset)
    {
        var packet = CreatePacketInstance();
        packet.SetReadWritePosition(readOffset);
        packet.SetSocketData(ref socketData);
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

    protected abstract T CreatePacketInstance();
}