using System;
using System.Collections.Generic;
using System.Linq;

public class RoomManager
{
    public static RoomManager Instance = new RoomManager();

    private Dictionary<Guid, RoomListEntity> _rooms = new Dictionary<Guid, RoomListEntity>();
    public IReadOnlyDictionary<Guid, RoomListEntity> Rooms => _rooms;

    public void Fill(RoomListEntity[] rooms)
    {
        _rooms = rooms.ToDictionary(x => x.RoomId, x => x);
        RoomChanged(_rooms);
    }

    public event Action<IReadOnlyDictionary<Guid, RoomListEntity>> RoomChanged = delegate { };
}
