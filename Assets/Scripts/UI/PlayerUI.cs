using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public void RespawnPlayer()
    {
        NetworkClientSend.PlayerRespawn();
    }
}
