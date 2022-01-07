using System.Collections.Generic;

public abstract class NetworkPacketHandlerBase<T> : IPacketHandleable where T : PacketBase, new()
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

    public virtual void HandleBytes(byte[] packetBytes, int readOffset)
    {
        var packet = new T();
        packet.SetReadWritePosition(readOffset);
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