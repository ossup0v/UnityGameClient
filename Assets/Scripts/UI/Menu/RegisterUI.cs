using UnityEngine;
using UnityEngine.UI;

public class RegisterUI : MonoBehaviour
{
    public GameObject RegisterMenu;
    public InputField LoginField;
    public InputField PasswordField;
    public InputField UsernameField;

    private void Awake()
    {
        PasswordField.text = "password" + Random.Range(1, 1000).ToString();
        UsernameField.text = "username" + Random.Range(1, 1000).ToString();
        LoginField.text = "login" + Random.Range(1, 1000).ToString();
    }

    public void Register()
    {
        NetworkManager.Instance.Username = UsernameField.text;
        NetworkClientSendServer.Register(LoginField.text, PasswordField.text, UsernameField.text, RegisterCallback);
    }

    private void RegisterCallback(bool result, string message)
    {
        if (!result)
        {
            UIManager.Instance.FailureUI.ShowMessage(message);
        }
        else
        {
            UIManager.Instance.RoomListUI.Menu.SetActive(true);
            UIManager.Instance.RoomListUI.enabled = true;
            RegisterMenu.SetActive(false);
        }
    }
}
