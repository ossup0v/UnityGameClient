using System;
using System.Text;

public static class PacketWriteHelper
{
    public static void Write(this PacketBase packetBase, int value)
    {
        var buffer = packetBase.GetBytes();
        var writePosition = packetBase.ReadWritePosition;
        var bytes = BitConverter.GetBytes(value);
        Array.Copy(bytes, 0, buffer, writePosition, bytes.Length);
        writePosition += bytes.Length;
        packetBase.SetReadWritePosition(writePosition);
        packetBase.SetBytes(buffer);
    }

    public static void Write(this PacketBase packetBase, Guid value)
    {
        foreach (var @int in Guid2Int(value))
        {
            Write(packetBase, @int);
        }
    }

    public static void Write(this PacketBase packetBase, string value)
    {
        Write(packetBase, value.Length);
        var buffer = packetBase.GetBytes();
        var writePosition = packetBase.ReadWritePosition;
        var bytes = Encoding.ASCII.GetBytes(value);
        Array.Copy(bytes, 0, buffer, writePosition, bytes.Length);
    }

    public static int[] Guid2Int(Guid value)
    {
        byte[] b = value.ToByteArray();
        int bint = BitConverter.ToInt32(b, 0);
        var bint1 = BitConverter.ToInt32(b, 4);
        var bint2 = BitConverter.ToInt32(b, 8);
        var bint3 = BitConverter.ToInt32(b, 12);
        return new[] { bint, bint1, bint2, bint3 };
    }
}
