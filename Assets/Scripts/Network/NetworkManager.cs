using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    public string Username;
    public NetworkClient ServerClient;
    public NetworkClient RoomClient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Debug.LogError($"{nameof(NetworkManager)} singletone error!");
            Destroy(this);
        }
    }
}
