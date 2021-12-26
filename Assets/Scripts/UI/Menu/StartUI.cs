using UnityEngine;
using UnityEngine.UI;

public class StartUI : MonoBehaviour
{
    public GameObject StartMenu;
    public InputField Localhost;
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

    private void SetUnActive()
    {
        Localhost.interactable = false;
        RemoteServer.interactable = false;
    }
}

