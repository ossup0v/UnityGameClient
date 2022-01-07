using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    public string Username;
    public int Team;
    public NetworkClient ServerClient;
    public NetworkClient RoomClient;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            Instance.Team = Random.Range(1, 2);
        }
        else if (Instance != this)
        {
            Debug.LogError($"{nameof(NetworkManager)} singletone error!");
            Destroy(this);
        }
    }
}
