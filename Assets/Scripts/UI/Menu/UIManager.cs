using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public StartUI StartUI;
    public RegisterUI RegisterUI;
    public MetaGameMenuUI MetaGameMenuUI;

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
        MetaGameMenuUI.MetaGameMenu.SetActive(true);
    }

    public void JoinGameRoom()
    {
        MetaGameMenuUI.JoinGameRoom();
        MetaGameMenuUI.MetaGameMenu.SetActive(false);
    }
}

