public abstract class PacketBase
{
    protected byte[] _packetBytes;
    
    public virtual int ReadWritePosition { get; protected set; }

    public virtual void SetBytes(byte[] packetBytes)
    {
        _packetBytes = packetBytes;
    }

    public virtual void SetReadWritePosition(int readWritePosition)
    {
        ReadWritePosition = readWritePosition;
    }

    public virtual byte[] GetBytes()
    {
        return _packetBytes;
    }

    public abstract void SerializePacket();
    public abstract void DeserializePacket();
}
