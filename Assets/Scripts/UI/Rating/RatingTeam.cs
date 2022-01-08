using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RatingTeam : MonoBehaviour
{
    [SerializeField] private RectTransform _playersParent;
    [SerializeField] private PlayerInfo _playerInfoPrefab;
    [SerializeField] private TextMeshProUGUI _teamName;

    private List<PlayerInfo> _players = new List<PlayerInfo>();
    public int ID { get; set; }

    public void InitTeam(int id)
    {
        ID = id;
        _teamName.text = "Team " + id.ToString();

    }
    public void AddPlayer(RatingEntity player)
    {
        PlayerInfo newPlayer = Instantiate(_playerInfoPrefab, _playersParent);
        newPlayer.Id = player.Id;
        _players.Add(newPlayer);
        newPlayer.UpdateInfo(player);
    }

    public void UpdatePlayerInfo(RatingEntity player)
    {
        PlayerInfo playerInfo = _players.Find(currentPlayer => currentPlayer.Id == player.Id);
        playerInfo.UpdateInfo(player);
    }
}
