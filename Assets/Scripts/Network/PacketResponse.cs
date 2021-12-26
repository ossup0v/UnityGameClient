using System;

public class PacketResponse : Packet
{
    /// <summary>
    /// unique id of packet
    /// </summary>
    public Guid UId;

    private PacketResponse() { }

    public static PacketResponse CreateFromPacket(Packet packet)
    {
        var response = new PacketResponse();

        response.UId = packet.ReadGuid();
        var bytesLength = packet.GetReadbleBuffer().Length - packet.GetReadPos();
        var bytes = new byte[bytesLength];

        Array.Copy(packet.GetReadbleBuffer(), packet.GetReadPos(), bytes, 0, bytesLength);

        response.SetBytes(bytes);

        return response;
    }
}
