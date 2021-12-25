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

        LoginMenu.SetActive(false);
    }

    private void LoginCallback(bool result)
    { 
        if (!result)
        {
            //retry here
        }
    }
}
