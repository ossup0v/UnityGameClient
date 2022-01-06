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
        var maxPlayerCount = 0;
        if (!int.TryParse(MaxPlayerCount.text, out maxPlayerCount))
        {
            maxPlayerCount = 16;
        }
        NetworkClientSendServer.CreateGameRoom(RoomMode.text, Title.text, maxPlayerCount);
        CreateRoomPerfab.SetActive(false);
        Source.SetActive(false);
        Destroy(this);
    }

    public void Cancel()
    {
        CreateRoomPerfab.SetActive(false);
    }

    public void DisableAll()
    {
        CreateRoomPerfab.SetActive(false);
        Source.SetActive(false);
        Destroy(this);
    }
}
