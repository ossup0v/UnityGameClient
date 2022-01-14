using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NetworkPacket(WelcomePacket.PacketID1, typeof(Refactor.ClientRoomNetworkPacketsReceiver))]
public sealed class WelcomePacketHandler : NetworkPacketHandler<WelcomePacket>
{
    protected override WelcomePacket CreatePacketInstance()
    {
        return new WelcomePacket();
    }
}

public sealed class WelcomePacket : PacketBase
{
    public const int PacketID1 = 1;
    public string Message { get; private set; }
    public Guid MyGuid { get; private set; }
    public int FromWho { get; private set; }

    public override void DeserializePacket()
    {
        Message = this.ReadString();
        MyGuid = this.ReadGuid();
        FromWho = this.ReadInt();
        Debug.Log(Message + " " + MyGuid + " " + FromWho);
        NetworkManager.Instance.ServerClient.MyId = MyGuid;
        NetworkManager.Instance.RoomClient.MyId = MyGuid;
    }

    public override void SerializePacket()
    {
        // var tmpOffset = 4;
        // SetReadWritePosition(tmpOffset);
        this.Write(PacketID1);
        this.Write(NetworkManager.Instance.RoomClient.MyId); // TODO: менять
        this.Write(NetworkManager.Instance.Username);
    }
}
