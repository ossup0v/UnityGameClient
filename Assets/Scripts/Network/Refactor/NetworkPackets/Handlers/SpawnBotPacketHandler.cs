using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[NetworkPacket(SpawnBotPacket.PacketID23, typeof(Refactor.ClientRoomNetworkPacketReceiver))]
public sealed class SpawnBotPacketHandler : NetworkPacketHandler<SpawnBotPacket>
{
    protected override SpawnBotPacket CreatePacketInstance()
    {
        return new SpawnBotPacket();
    }
}

public sealed class SpawnBotPacket : PacketBase
{
    public const int PacketID23 = 23;
    public Guid BotID { get; private set; }
    public Vector3 BotPosition { get; private set; }
    public int BotWeaponKind { get; private set; }

    public override void DeserializePacket()
    {
        BotID = this.ReadGuid();
        BotPosition = this.ReadVector3();
        BotWeaponKind = this.ReadInt();

        ThreadManager.ExecuteOnMainThread(() =>
        {
            GameManager.Instance.SpawnBot(BotID, (WeaponKind)BotWeaponKind, BotPosition);
        });
    }

    public override void SerializePacket()
    {
        
    }
}