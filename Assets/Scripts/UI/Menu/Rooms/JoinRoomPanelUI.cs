using UnityEngine;

public class JoinRoomPanelUI : MonoBehaviour
{
    public GameObject CancelButton;

    private void Awake()
    {
        CancelButton.SetActive(false);
    }

    public void StartSearchRoomGame()
    {
        NetworkClientSendServer.StartSearchRoomGame();
        CancelButton.SetActive(true);
    }

    public void Cancel()
    {
        NetworkClientSendServer.CancelSearchRoomGame();
        CancelButton.SetActive(false);
    }

    private void OnEnable()
    {
        CancelButton.SetActive(false);
    }
}