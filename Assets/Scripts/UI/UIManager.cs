using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject StartMenu;
    public InputField UsernameFiled;
    public InputField Localhost;
    public InputField RemoteServer;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"{nameof(NetworkClient)} singletone error!");
            Destroy(this);
        }

        Instance.Localhost.text = "127.0.0.1";
        Instance.RemoteServer.text = "3.66.29.169";
    }

    public void ConnectToServer()
    {
        StartMenu.SetActive(false);
        SetUnActive();
        NetworkClient.Instance.ConnectToServer(RemoteServer.text);
    }
    public void ConnectToLocalServer()
    {
        StartMenu.SetActive(false);
        SetUnActive();
        NetworkClient.Instance.ConnectToServer(Localhost.text);
    }

    private void SetUnActive()
    { 
        UsernameFiled.interactable = false;
        Localhost.interactable = false;
        RemoteServer.interactable = false;
    }
}
