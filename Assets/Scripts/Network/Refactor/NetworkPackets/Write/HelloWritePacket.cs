public class HelloWritePacket : WritePacketBase
{
    public const int PacketID_1 = 1;
    public override int PacketID => PacketID_1;

    public override void SerializePacket()
    {
    }
}