using System;
using UnityEngine;
using UnityEngine.UI;

public class InterRoomPanelUI : MonoBehaviour
{
    private GameObject _source;
    private GameObject _sourceParent;
    public Text Owner;
    public Text Mode;
    public Text PlayerCount;
    private Guid _roomId;

    public void Fill(GameObject source, GameObject sourceParent, Guid roomId, string owner, string mode, string playerCount)
    {
        _source = source;
        _sourceParent = sourceParent;
        _roomId = roomId;
        Owner.text = owner;
        Mode.text = mode;
        PlayerCount.text = playerCount;
    }

    public void Cancel()
    {
        _source.SetActive(false);
    }

    public void JoinGameRoom()
    {
        NetworkClientSendServer.JoinGameRoom(_roomId);
        _source.SetActive(false);
        _sourceParent.SetActive(false);
    }
}