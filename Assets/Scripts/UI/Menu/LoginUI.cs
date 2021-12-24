using UnityEngine;
using UnityEngine.UI;

public class LoginUI : MonoBehaviour
{
    public GameObject LoginMenu;
    public InputField LoginField;
    public InputField PasswordField;

    public void Login()
    {
        NetworkClientSendServer.Login(LoginField.text, PasswordField.text);

        LoginMenu.SetActive(false);
    }
}
