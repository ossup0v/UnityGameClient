using Refactor;

public abstract class ReadPacketBase
{
    protected SocketData _socketData;
    protected byte[] _packetBytes;

    public System.Guid GUID { get; set; }
    public virtual int ReadPosition { get; protected set; }

    public virtual void SetSocketData(ref SocketData socketData)
    {
        _socketData = socketData;
    }

    public virtual void SetBytes(byte[] packetBytes)
    {
        _packetBytes = packetBytes;
    }

    public virtual byte[] GetBytes()
    {
        return _packetBytes;
    }

    public virtual void SetReadPosition(int readPosition)
    {
        ReadPosition = readPosition;
    }

    public virtual void Reset()
    {
        ReadPosition = 0;
    }

    public virtual void DeserializePacket()
    {
        Reset();
        GUID = this.ReadGuid();
    }
}
