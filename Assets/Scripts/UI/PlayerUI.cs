using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public void RespawnPlayer()
    {
        NetworkClientSendRoom.PlayerRespawn();
    }
}
