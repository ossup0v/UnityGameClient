using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListUI : MonoBehaviour
{
    public GameObject Menu;
    public GameObject EntityTemplate;
    public Transform PanelTransform;

    public GameObject EnterRoomPanel;

    private RoomListEntity[] _rooms = new RoomListEntity[]
    {
        new RoomListEntity()
        {
            UserOwner = "UserOwner1",
            Title = "Title1",
            Mode = "PVP",
            UserInRoomCount = 2,
            AvailableUserCount = 4,
            RoomId = Guid.NewGuid(),
        },
        new RoomListEntity()
        {
            UserOwner = "UserOwner2",
            Title = "Title2",
            Mode = "PVP",
            UserInRoomCount = 5,
            AvailableUserCount = 8,
            RoomId = Guid.NewGuid()
        },
        new RoomListEntity()
        {
            UserOwner = "UserOwner3",
            Title = "Title3",
            Mode = "PVE",
            UserInRoomCount = 9,
            AvailableUserCount = 10,
            RoomId = Guid.NewGuid()
        }
    };


    private void Start()
    {
        OnRoomListChanged(RoomManager.Instance.Rooms);
        RoomManager.Instance.RoomChanged += OnRoomListChanged;
        

        Destroy(EntityTemplate);
    }

    private void OnRoomListChanged(IReadOnlyDictionary<Guid, RoomListEntity> rooms)
    {
        var N = 1;
        
        foreach (var room in rooms.Values)
        {
            var instance = Instantiate(EntityTemplate, transform);
            instance.GetComponent<RoomListEntityUI>().Fill(room, N);
            instance.GetComponent<Button>().AddListener(room, ClickCallback);

            N++;
        }
    }

    private void ClickCallback(RoomListEntity room)
    {
        var panel = Instantiate(EnterRoomPanel, PanelTransform);
        panel.SetActive(true);
        panel.GetComponent<InterRoomPanelUI>()
            .Fill(panel, Menu, room.RoomId, room.UserOwner, room.Mode, $"{room.UserInRoomCount}/{room.AvailableUserCount}");

        Debug.Log(room.RoomId);
    }
}