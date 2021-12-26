using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public GameObject LoginMenu;
    public InputField LoginField;
    public InputField PasswordField;

    public void Login()
    {
        NetworkClientSendServer.Login(LoginField.text, PasswordField.text, LoginCallback);
    }

    private void LoginCallback(bool result, string message)
    { 
        if (!result)
        {
            UIManager.Instance.FailureUI.ShowMessage(message);
        }
        else
        {
            UIManager.Instance.RoomListUI.Menu.SetActive(true);
            UIManager.Instance.RoomListUI.enabled = true;
            LoginMenu.SetActive(false);
        }
    }
}
