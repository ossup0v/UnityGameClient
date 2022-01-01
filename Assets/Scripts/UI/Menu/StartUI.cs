using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject StartMenu;
    public InputField Localhost;
    public InputField LocalhostRoom;
    public InputField RemoteServer;

    private void Awake()
    {
        StartMenu.SetActive(true);
        Localhost.text = "127.0.0.1";
        RemoteServer.text = "3.66.29.169";
    }

    public void ConnectToServer()
    {
        StartMenu.SetActive(false);
        SetUnActive();
        NetworkManager.Instance.ServerClient.ConnectToServer(RemoteServer.text);
    }

    public void ConnectToLocalServer()
    {
        StartMenu.SetActive(false);
        SetUnActive();
        NetworkManager.Instance.ServerClient.ConnectToServer(Localhost.text);
    }

    public void ConnectToRoom()
    {
        StartMenu.SetActive(false);
        SetUnActive();

        var defaultPort = 26954;
        if (int.TryParse(LocalhostRoom.text, out var port))
            port = defaultPort;

        NetworkManager.Instance.RoomClient.ConnectToServer("127.0.0.1", port);
    }

    private void SetUnActive()
    {
        Localhost.interactable = false;
        RemoteServer.interactable = false;
    }
}

