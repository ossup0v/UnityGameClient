using System;

[AttributeUsage(AttributeTargets.Class)]
public class NetworkPacketAttribute : Attribute
{
    public int PacketNumber { get; private set; }
    public Type PacketHandler { get; private set; }

    public NetworkPacketAttribute(int packetNumber, Type packetHandler)
    {
        PacketNumber = packetNumber;
        PacketHandler = packetHandler;
    }
}