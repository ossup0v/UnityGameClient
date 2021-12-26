using UnityEngine;
using UnityEngine.UI;

public class RegisterUI : MonoBehaviour
{
    public GameObject RegisterMenu;
    public InputField LoginField;
    public InputField PasswordField;
    public InputField UsernameField;

    public void Register()
    {
        NetworkManager.Instance.Username = UsernameField.text;
        NetworkClientSendServer.Register(LoginField.text, PasswordField.text, UsernameField.text, RegisterCallback);

    }

    private void RegisterCallback(bool result)
    {
        if (!result)
        {
            //retry here
        }

        RegisterMenu.SetActive(false);
    }
}
