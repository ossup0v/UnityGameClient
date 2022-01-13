using System;
using System.Collections;
using System.Reflection;
using System.Text;
using UnityEngine;

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

public static class PacketReadHelper
{
    public static string ReadString(this PacketBase packetBase)
    {
        var stringLenght = ReadInt(packetBase);
        var buffer = packetBase.GetBytes();
        var readPosition = packetBase.ReadWritePosition;
        if (readPosition + stringLenght <= buffer.Length)
        {
            var stringValue = Encoding.ASCII.GetString(buffer, readPosition, stringLenght);
            readPosition += stringLenght;
            packetBase.SetReadWritePosition(readPosition);
            return stringValue;
        }
        else
        {
            Logger.WriteError(nameof(ReadString), $"Can't read string with lenght {stringLenght}, buffer lenght is {buffer.Length} and read position is {readPosition}");
            return default;
        }
    }

    public static int ReadInt(this PacketBase packetBase)
    {
        var buffer = packetBase.GetBytes();
        var typeSize = 4;
        var readPosition = packetBase.ReadWritePosition;
        if (readPosition + typeSize <= buffer.Length)
        {
            var value = BitConverter.ToInt32(buffer, readPosition);
            readPosition += typeSize;
            packetBase.SetReadWritePosition(readPosition);
            return value;
        }
        else
        {
            Logger.WriteError(nameof(ReadInt), $"Can't read int 32, buffer lenght is {buffer.Length} and read position is {readPosition}");
            return default;
        }
    }

    public static Guid ReadGuid(this PacketBase packetBase)
    {
        var value = ReadInt(packetBase);
        var value1 = ReadInt(packetBase);
        var value2 = ReadInt(packetBase);
        var value3 = ReadInt(packetBase);
        return Int2Guid(value, value1, value2, value3);
    }

    public static Guid Int2Guid(int value, int value1, int value2, int value3)
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        BitConverter.GetBytes(value1).CopyTo(bytes, 4);
        BitConverter.GetBytes(value2).CopyTo(bytes, 8);
        BitConverter.GetBytes(value3).CopyTo(bytes, 12);
        return new Guid(bytes);
    }

    public static Vector3 ReadVector3(this PacketBase packetBase)
    {
        var x = ReadFloat(packetBase);
        var y = ReadFloat(packetBase);
        var z = ReadFloat(packetBase);
        return new Vector3(x, y, z);
    }

    public static float ReadFloat(this PacketBase packetBase)
    {
        var buffer = packetBase.GetBytes();
        var typeSize = 4;
        var readPosition = packetBase.ReadWritePosition;
        if (readPosition + typeSize <= buffer.Length)
        {
            var value = BitConverter.ToSingle(buffer, readPosition);
            readPosition += typeSize;
            packetBase.SetReadWritePosition(readPosition);
            return value;
        }
        else
        {
            Logger.WriteError(nameof(ReadFloat), $"Can't read flaot, buffer lenght is {buffer.Length} and read position is {readPosition}");
            return default;
        }
    }


}