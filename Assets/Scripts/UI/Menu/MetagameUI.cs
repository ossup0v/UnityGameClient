using UnityEngine;

public class MetagameUI : MonoBehaviour
{
    public GameObject Menu;
    public GameObject CreateRoomPanel;

    public void CreateRoom()
    {
        CreateRoomPanel.SetActive(true);
        CreateRoomPanel.GetComponent<CreateRoomPanelUI>().Source = Menu;
    }
}
