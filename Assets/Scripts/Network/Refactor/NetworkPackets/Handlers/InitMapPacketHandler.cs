using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NetworkPacket(InitMapPacket.PacketID22, typeof(Refactor.ClientRoomNetworkPacketReceiver))]
public sealed class InitMapPacketHandler : NetworkPacketHandler<InitMapPacket>
{
    protected override InitMapPacket CreatePacketInstance()
    {
        return new InitMapPacket();
    }
}

public sealed class InitMapPacket : PacketBase
{
    public const int PacketID22 = 22;
    

    public override void DeserializePacket()
    {
        var mapData = this.ReadString();
        ThreadManager.ExecuteOnMainThread(() =>
        {
            MapManager.Instance.InitializeMap(mapData);
        });
    }

    public override void SerializePacket()
    {
    }
}
