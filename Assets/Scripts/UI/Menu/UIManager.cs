using UnityEngine;

public class UIManager : MonoBehaviour
{
#warning delete this shit please
    public static UIManager Instance;
    public StartUI StartUI;
    public LoginUI LoginUI;
    public RegisterUI RegisterUI;
    public InterRoomPanelUI InterRoomPanelUI;
    public RoomListUI RoomListUI;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError($"awake called twise! {nameof(UIManager)}");
        }
    }

    public void ConnectToServer()
    {
        StartUI.ConnectToServer();
        RegisterUI.RegisterMenu.SetActive(true);
    }

    public void ConnectToLocalServer()
    {
        StartUI.ConnectToLocalServer();
        RegisterUI.RegisterMenu.SetActive(true);
    }

    public void Register()
    {
        RegisterUI.Register();
        RegisterUI.RegisterMenu.SetActive(false);

        RoomListUI.Menu.SetActive(true);
        RoomListUI.enabled = true;
    }

    public void JoinGameRoom()
    {
        InterRoomPanelUI.JoinGameRoom();
        DisableAll();
    }

    public void Login()
    {
        LoginUI.Login();
        LoginUI.LoginMenu.SetActive(false);

        RoomListUI.Menu.SetActive(true);
        RoomListUI.enabled = true;
    }

    private void DisableAll()
    {
        StartUI.StartMenu.SetActive(false);
        LoginUI.LoginMenu?.SetActive(false);
        RegisterUI.RegisterMenu.SetActive(false);
        RoomListUI.Menu?.SetActive(false);
    }
}

