using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NetworkPacket(InitMapPacket.PacketID22, typeof(Refactor.ClientRoomNetworkPacketsReceiver))]
public sealed class InitMapPacketHandler : NetworkReadPacketHandler<InitMapPacket>
{
    protected override InitMapPacket CreatePacketInstance()
    {
        return new InitMapPacket();
    }
}

public sealed class InitMapPacket : ReadPacketBase
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
}
