using UnityEngine;

public class MetaGameMenuUI : MonoBehaviour
{
    public GameObject MetaGameMenu;

    public void JoinGameRoom()
    {
        NetworkClientSendServer.JoinGameRoom();
    }
}
