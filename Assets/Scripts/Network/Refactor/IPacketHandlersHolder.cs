public interface IPacketHandlersHolder
{
    T GetPacketHandlerByPacketID<T>(int packetID) where T : class, IPacketHandleable;
}