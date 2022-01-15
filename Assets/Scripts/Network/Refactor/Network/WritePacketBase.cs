using Refactor;

public abstract class WritePacketBase
{
    protected SocketData _socketData;
    protected byte[] _packetBytes;

    public System.Guid GUID { get; set; }
    public virtual int WritePosition { get; protected set; }
    public int Lenght => WritePosition;

    public WritePacketBase(int bufferSize)
    {
        _packetBytes = new byte[bufferSize];
    }

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

    public virtual void SetWritePosition(int readPosition)
    {
        WritePosition = readPosition;
    }

    public virtual void Reset()
    {
        WritePosition = 0;
    }

    public virtual void SerializePacket()
    {
        Reset();
        this.Write(GUID);
    }
}
