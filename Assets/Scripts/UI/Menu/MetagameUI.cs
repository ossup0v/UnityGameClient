using UnityEngine;

public class MetagameUI : MonoBehaviour
{
    public static MetagameUI Instance;

    private void Awake()
    {
        Instance = this;
    }

    public GameObject Menu;
    public GameObject CreateRoomPanel;
    public JoinRoomPanelUI JoinGame;
    public CreateRoomPanelUI CreateRoomPanelUI;

    public void CreateRoom()
    {
        CreateRoomPanel.SetActive(true);
        CreateRoomPanelUI = CreateRoomPanel.GetComponent<CreateRoomPanelUI>();
        CreateRoomPanelUI.Source = Menu;
    }

    public void DisableAll()
    {
        Menu.SetActive(false);
        JoinGame.enabled = false;
        CreateRoomPanel.SetActive(false);
        CreateRoomPanelUI?.DisableAll();
    }

    public void EnableAll()
    {
        Menu?.SetActive(true);
        JoinGame.enabled = true;
        //CreateRoomPanel?.SetActive(true);
        //CreateRoomPanelUI?.DisableAll();
    }
}
