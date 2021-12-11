using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public GameObject StartMenu;
    public GameObject GameUI;
    public InputField UsernameFiled;

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
    }

    public void ConnectToServer()
    {
        StartMenu.SetActive(false);
        UsernameFiled.interactable = false;
        NetworkClient.Instance.ConnectToServer();
        GameUI.SetActive(true);
    }
}
