using UnityEngine;
using UnityEngine.UI;

public class CreateRoomPanelUI : MonoBehaviour
{
    public GameObject CreateRoomPerfab;
    public GameObject Source;
    public InputField MaxPlayerCount;
    public InputField RoomMode;
    public InputField Title;

    public void CreateGameRoom()
    {
        NetworkClientSendServer.CreateGameRoom(RoomMode.text, Title.text, MaxPlayerCount.text);
        CreateRoomPerfab.SetActive(false);
        Source.SetActive(false);
        Destroy(this);
    }

    public void Cancel()
    {
        NetworkClientSendServer.CreateGameRoom(RoomMode.text, Title.text, MaxPlayerCount.text);
        CreateRoomPerfab.SetActive(false);
        Destroy(this);
    }
}
