using System;
using UnityEngine;
using UnityEngine.UI;

public class RoomListEntityUI : MonoBehaviour
{
    public Text Number;
    public Text UserOwner;
    public Text Title;
    public Text UserInRoomCount;
    public Text Mode;
    public Guid RoomId;

    public void Fill(RoomListEntity entity, int number)
    {
        Number.text = number.ToString();
        UserOwner.text = entity.UserOwner;
        Title.text = entity.Title;
        Mode.text = entity.Mode;
        UserInRoomCount.text = $"{entity.UserInRoomCount}/{entity.AvailableUserCount}";
        RoomId = entity.RoomId;
    }
}
