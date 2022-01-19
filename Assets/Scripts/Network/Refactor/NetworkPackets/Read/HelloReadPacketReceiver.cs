namespace Refactor
{
    public class HelloReadPacketReceiver : ClientPacketReceiverMainThreadBase<HelloReadPacket>
    {
        public HelloReadPacketReceiver(INetworkClientPacketsSender networkClientPacketsSender, IPacketHandlersHolder packetHandlersHolder) : base(networkClientPacketsSender, packetHandlersHolder)
        {
        }

        protected override int _packetID => HelloReadPacket.PacketID_1;

        protected override void ReceivePacketMainThread(HelloReadPacket packet)
        {
            Logger.WriteLog("", "received hello packet in main thread");
        }
    }
}