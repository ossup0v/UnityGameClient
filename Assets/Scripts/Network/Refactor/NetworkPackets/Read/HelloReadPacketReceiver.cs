using System;

namespace Refactor
{
    public class HelloReadPacketReceiver : ClientPacketReceiverMainThreadBase<HelloReadPacket>
    {
        public event Action PacketReceived;

        public HelloReadPacketReceiver(INetworkClientPacketsSender networkClientPacketsSender, IPacketHandlersHolder packetHandlersHolder) : base(networkClientPacketsSender, packetHandlersHolder)
        {
        }

        protected override int _packetID => HelloReadPacket.PacketID_1;

        protected override void ReceivePacketMainThread(HelloReadPacket packet)
        {
            Logger.WriteLog("", "received hello packet in main thread GUID " + packet.ClientID);
            PacketReceived?.Invoke();
        }
    }
}