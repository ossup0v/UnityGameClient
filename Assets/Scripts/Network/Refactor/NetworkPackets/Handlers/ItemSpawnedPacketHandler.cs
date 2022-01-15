using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NetworkPacket(ItemSpawnedPacket.PacketID9, typeof(Refactor.ClientRoomNetworkPacketsReceiver))]
public sealed class ItemSpawnedPacketHandler : NetworkReadPacketHandler<ItemSpawnedPacket>
{
    protected override ItemSpawnedPacket CreatePacketInstance()
    {
        return new ItemSpawnedPacket();
    }
}

public sealed class ItemSpawnedPacket : ReadPacketBase
{
    public const int PacketID9 = 9;
    public int SpawnerID { get; private set; }

    public override void DeserializePacket()
    {
        SpawnerID = this.ReadInt();
        ThreadManager.ExecuteOnMainThread(() =>
        {
            GameManager.ItemSpawners[SpawnerID].ItemSpawned();
        });
        Debug.Log(SpawnerID);
    }
}
