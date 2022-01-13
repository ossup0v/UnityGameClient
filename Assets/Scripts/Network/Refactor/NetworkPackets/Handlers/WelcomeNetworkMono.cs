using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WelcomeNetworkMono : NetworkMonoBehaviour<WelcomePacket>
{
    protected override IPacketHandlersHolder _packetHandlersHolder => Refactor.NetworkClient.S_NetworkClient.NetworkClientReceiver;

    protected override int _packetID => WelcomePacket.PacketID1;

    public override void ReceivePacket(WelcomePacket packet)
    {
        ThreadManager.ExecuteOnMainThread(() =>
        {
            var responsePacket = new WelcomePacket();       
            responsePacket.SetBytes(new byte[1024]); // TODO: переделать 
            Refactor.NetworkClient.S_NetworkClient.Send(responsePacket);
        });
    }
}
