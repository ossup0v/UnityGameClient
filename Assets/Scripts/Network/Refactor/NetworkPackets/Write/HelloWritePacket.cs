public class HelloWritePacket : WritePacketBase
{
    public const int PacketID_1 = 1;
    public int SomeValue { get; set; }

    public override int PacketID => PacketID_1;

    public override void SerializePacket()
    {
        this.Write(SomeValue);
    }
}